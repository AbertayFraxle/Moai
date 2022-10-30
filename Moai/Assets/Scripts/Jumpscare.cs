using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumpscare : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    private float timer = 0;
    private float finishDeath = 5;
        
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= finishDeath)
        {
            SceneManager.LoadScene(0);
        }
    }
}
