using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject heldItem;
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

    bool changeText;
    private void Start()
    {
        text.text = " ";
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
        if (interactables.Count > 0 || changeText)
        {
            changeText = false;
            closestInteractable = CheckForClosestInteractable();
            if (closestInteractable != previousInteractable)
            {
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
                    }
                    else
                    {
                        text.text = itemRequired + " required.";
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
                    StopAllCoroutines();
                    StartCoroutine(PutItemAway(obstacle.itemRequired));
                    if (obstacle.gameObject.name == "Cell Door")
                    {
                        changeText = true;
                        return;
                    }
                }
                else
                {
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
            }
        }
    } 

    private IEnumerator PutItemAway(string itemName)
    {
        float timer = 0;
        GameObject item = null;
        heldItem.SetActive(true);
        switch (itemName)
        {
            case "Cell Key":
                item = Instantiate(cellKeyPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Jerry Can":
                item = Instantiate(jerryCanPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Boat Key":
                item = Instantiate(boatKeyPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Boltcutters":
                item = Instantiate(wirecuttersPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
        }
        heldItem.transform.localPosition = heldItemPosition;
        while (timer < handMoveTime)
        {
            heldItem.transform.localPosition = Vector3.Lerp(heldItemPosition, putAwayPosition, timer / handMoveTime);
            timer += Time.deltaTime;
            yield return null;
        }
        heldItem.SetActive(false);
        Destroy(item);
    }

    private IEnumerator PullItemOut(string itemName)
    {
        float timer = 0;
        GameObject item = null;
        heldItem.SetActive(true);
        switch (itemName)
        {
            case "Cell Key":
                item = Instantiate(cellKeyPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Jerry Can":
                item = Instantiate(jerryCanPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Boat Key":
                item = Instantiate(boatKeyPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
            case "Boltcutters":
                item = Instantiate(wirecuttersPrefab, heldItem.transform.position, Quaternion.identity, heldItem.transform);
                break;
        }
        heldItem.transform.localPosition = putAwayPosition;
        while (timer < handMoveTime)
        {
            heldItem.transform.localPosition = Vector3.Lerp(putAwayPosition, heldItemPosition, timer / handMoveTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
