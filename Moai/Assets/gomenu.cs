using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gomenu : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void END()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
