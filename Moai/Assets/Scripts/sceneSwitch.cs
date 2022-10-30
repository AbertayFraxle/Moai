using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitch : MonoBehaviour
{
    void OnTriggerEnter(Collider Other)
    {
        SceneManager.LoadScene(2);
    }
}
