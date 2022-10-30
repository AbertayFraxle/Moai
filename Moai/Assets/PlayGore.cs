using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayGore : MonoBehaviour
{
    private bool playSound;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        playSound = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playSound)
        {
            Vector3 newpoint = cam.WorldToViewportPoint(this.transform.position);
            float distance = (this.transform.position - cam.transform.position).magnitude;

            if (newpoint.x < 1 && newpoint.x > 0 && newpoint.y < 1 && newpoint.y > 0)
            {
                if (distance < 10f)
                {
                    float angle = Vector3.Angle(cam.transform.forward, this.transform.position - cam.transform.position);
                    if (Mathf.Abs(angle) < 90)
                    {
                        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("effects", 1);
                        this.GetComponent<AudioSource>().Play();
                        playSound = false;
                    }
                }
            }
        }
    }
}
