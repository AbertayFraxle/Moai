using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public GameObject settings;

    [SerializeField]
    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void optionButton()
    {
        settings.SetActive(true);
        main.SetActive(false);
    }
}
