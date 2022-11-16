using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject heldItemParent;
    GameObject heldItem;
    [SerializeField] Vector3 heldItemPosition;
    [SerializeField] Vector3 putAwayPosition;
    [SerializeField] float handMoveTime = 1;

    List<GameObject> interactables = new List<GameObject>();
    [SerializeField] string interactKeybind = "e";
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Transform cameraTransform;
    GameObject previousInteractable;

    [SerializeField] string itemTag;
    [SerializeField] string obstacleTag;
    [SerializeField] string doorTag;
    List<string> inventory = new List<string>();

    [SerializeField] GameObject jerryCanPrefab;
    [SerializeField] GameObject cellKeyPrefab;
    [SerializeField] GameObject boatKeyPrefab;
    [SerializeField] GameObject wirecuttersPrefab;

    ViewObjectives objectives;

    bool changeText;
    private void Start()
    {
        text.text = " ";
        objectives = GetComponent<ViewObjectives>();
    }

    private void OnDrawGizmos()
    { /*
        Gizmos.DrawLine(cameraTransform.position, cameraTransform.forward * 45);
        for (int i = 0; i < interactables.Count; i++)
        {
            Gizmos.DrawLine(interactables[i].transform.position, transform.position);
        } */
    }

    private void Update()
    {
        GameObject closestInteractable = null;
        if (interactables.Count > 0)
        {      
            closestInteractable = CheckForClosestInteractable();
            if (closestInteractable != previousInteractable || changeText)
            {
                changeText = false;
                if (closestInteractable.tag == itemTag)
                {
                    text.text = "[E] Pick up " + closestInteractable.name;
                }
                else if (closestInteractable.tag == obstacleTag)
                {
                    string itemRequired = closestInteractable.GetComponent<Obstacle>().itemRequired;
                    if (inventory.Contains(itemRequired))
                    {
                        StartCoroutine(PullItemOut(itemRequired));
                        text.text = "[E] Use " + itemRequired;
                        if (itemRequired == "Jerry Can (Empty)")
                        {
                            text.text = "[E] Fill Jerry Can";
                        }
                    }
                    else
                    {
                        if (inventory.Contains("Jerry Can (Full)") && itemRequired == "Jerry Can (Empty)")
                        {
                            text.text = "Jerry can is full";
                        }
                        else
                        {
                            text.text = itemRequired + " required.";
                        }
                    }
                    if (closestInteractable.name == "Boat")
                    {
                        if (closestInteractable.GetComponent<Boat>().isUnlocked)
                        {
                            text.text = "[E] Escape";
                        }
                    }
                }
                else if (closestInteractable.tag == doorTag)
                {
                    DoorTest door = closestInteractable.GetComponent<DoorTest>();
                    if (door.isOpen)
                    {
                        text.text = "[E] Close door";
                    }
                    else
                    {
                        text.text = "[E] Open door";
                    }
                }
                previousInteractable = closestInteractable;
            }
        }

        if (Input.GetKeyDown(interactKeybind) && closestInteractable != null)
        {
            if (closestInteractable.tag == itemTag)
            {
                inventory.Add(closestInteractable.name);
                StartCoroutine(PutItemAway(closestInteractable.name));
            }
            else if (closestInteractable.tag == obstacleTag)
            {
                Obstacle obstacle = closestInteractable.GetComponent<Obstacle>();
                if (inventory.Contains(obstacle.itemRequired))
                {
                    inventory.Remove(obstacle.itemRequired);
                    obstacle.UseItem();
                    Destroy(heldItem);
                    StopAllCoroutines();
                    StartCoroutine(PutItemAway(obstacle.itemRequired));
                    if (obstacle.name == "Cell Door")
                    {
                        changeText = true;
                        return;
                    }
                    if (obstacle.name == "Fuel Tank")
                    {
                        changeText = true;
                        objectives.fillJerry();
                        inventory.Add("Jerry Can (Full)");
                        return;
                    }
                    if (obstacle.name == "chain")
                    {
                        
                        return;
                    }
                    if (obstacle.name == "Boat")
                    {
                        
                        if (obstacle.GetComponent<Boat>().isUnlocked)
                        {
                            //WIN
                            Debug.Log("WIN");
                        }
                        else
                        {
                           
                        }
                        changeText = true;
                        return;
                    }
                }
                else
                {
                    if (obstacle.name == "Boat")
                    {
                        if (obstacle.GetComponent<Boat>().isUnlocked)
                        {
                            SceneManager.LoadScene("Winning",LoadSceneMode.Single);
                        }
                        
                        changeText = true;
                        return;
                    }
                    return;
                }
            }
            else if (closestInteractable.tag == doorTag)
            {
                changeText = true;
                closestInteractable.GetComponent<DoorTest>().ChangeDoorState();
                return;
            }
            else
            {
                Debug.LogError("Interactable tag not found");
                return;
            }
            interactables.Remove(closestInteractable);
            if (interactables.Count <= 0)
            {
                text.text = " ";
            }
            Destroy(closestInteractable);
        }
    }

    
    private GameObject CheckForClosestInteractable()
    {
        List<float> angles = new List<float>();
        for (int i = 0; i < interactables.Count; i++)
        {
            angles.Add(Vector3.Angle(cameraTransform.forward * 45, interactables[i].transform.position - transform.position));
        }

        float minValue = angles.Min();
        int index = angles.IndexOf(minValue);
        return interactables[index];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == itemTag || other.tag == obstacleTag || other.tag == doorTag)
        {
            interactables.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == itemTag || other.tag == obstacleTag || other.tag == doorTag)
        {
            interactables.Remove(other.gameObject);
            if (interactables.Count == 0)
            {
                text.text = " ";
                previousInteractable = null;
                if (heldItem != null)
                {
                    StartCoroutine(PutItemAway(heldItem.name));
                }
            }
        }
    } 

    private IEnumerator PutItemAway(string itemName)
    {
        float timer = 0;
        heldItemParent.SetActive(true);
        switch (itemName)
        {
            case "Cell Key":
                heldItem = Instantiate(cellKeyPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Jerry Can (Full)":
                heldItem = Instantiate(jerryCanPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;

            case "Jerry Can (Empty)":
                objectives.gotJerry();
                heldItem = Instantiate(jerryCanPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Boat Key":
                objectives.foundKey();
                heldItem = Instantiate(boatKeyPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Boltcutters":
                objectives.increaseDone();
                heldItem = Instantiate(wirecuttersPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
        }
        heldItemParent.transform.localPosition = heldItemPosition;
        while (timer < handMoveTime)
        {
            heldItemParent.transform.localPosition = Vector3.Lerp(heldItemPosition, putAwayPosition, timer / handMoveTime);
            timer += Time.deltaTime;
            yield return null;
        }
        heldItemParent.SetActive(false);
        Destroy(heldItem);
    }

    private IEnumerator PullItemOut(string itemName)
    {
        if (heldItem != null) { Destroy(heldItem); }
        float timer = 0;
        heldItemParent.SetActive(true);
        switch (itemName)
        {
            case "Cell Key":
                heldItem = Instantiate(cellKeyPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Jerry Can (Empty)":
                
                heldItem = Instantiate(jerryCanPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Jerry Can (Full)":
                heldItem = Instantiate(jerryCanPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Boat Key":
                objectives.usedKey();
                heldItem = Instantiate(boatKeyPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
            case "Boltcutters":
                objectives.boatHouseOpen();
                heldItem = Instantiate(wirecuttersPrefab, heldItemParent.transform.position, Quaternion.identity, heldItemParent.transform);
                break;
        }
        heldItemParent.transform.localPosition = putAwayPosition;
        while (timer < handMoveTime)
        {
            heldItemParent.transform.localPosition = Vector3.Lerp(putAwayPosition, heldItemPosition, timer / handMoveTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
