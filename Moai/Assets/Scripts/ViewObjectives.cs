using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewObjectives : MonoBehaviour
{
    [SerializeField]
    public GameObject objectiveSheet;

    public TextMeshProUGUI objective1, objective2, objective3, objective4, objective5, objective6;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!objectiveSheet.active)
            {
                objectiveSheet.SetActive(true);
            }else
            {
                objectiveSheet.SetActive(false);
            }
        }
    }

    public void boatHouseOpen()
    {
        objective1.fontStyle = FontStyles.Strikethrough;
    }

    public void gotJerry()
    {
        objective2.fontStyle = FontStyles.Strikethrough;
    }

    public void fillJerry()
    {
        objective3.fontStyle = FontStyles.Strikethrough;
    }

    public void fuelBoat()
    {
        objective4.fontStyle = FontStyles.Strikethrough;
    }

    public void foundKey()
    {
        objective5.fontStyle = FontStyles.Strikethrough;
    }

    public void usedKey()
    {
        objective6.fontStyle = FontStyles.Strikethrough;
    }

}
