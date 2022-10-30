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
        objective3.fontStyle = FontStyles.Strikethrough;
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
}
