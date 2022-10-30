using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gomenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void END()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
