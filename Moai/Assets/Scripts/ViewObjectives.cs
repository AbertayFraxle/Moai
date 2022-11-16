using TMPro;
using UnityEngine;

public class ViewObjectives : MonoBehaviour
{
    [SerializeField]
    public GameObject objectiveSheet;

    public TextMeshProUGUI objective1, objective2, objective3, objective4, objective5, objective6;
    int done;

    // Start is called before the first frame update
    void Start()
    {
        done = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!objectiveSheet.active)
            {
                objectiveSheet.SetActive(true);
            }
            else
            {
                objectiveSheet.SetActive(false);
            }
        }
    }

    public void increaseDone()
    {
        done++;
    }
    public void boatHouseOpen()
    {
        objective1.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public void gotJerry()
    {
        objective2.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public void fillJerry()
    {
        objective3.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public void fuelBoat()
    {
        objective4.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public void foundKey()
    {
        objective5.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public void usedKey()
    {
        objective6.fontStyle = FontStyles.Strikethrough;
        done++;
    }

    public int getDone()
    {
        return done;
    }

}
