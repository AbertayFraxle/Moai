using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float minOffTime;
    public float maxOffTime;
    public float minOnTime;
    public float maxOnTime;

    // Update is called once per frame
    void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }   
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        //Turn off light
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(minOffTime, maxOffTime);
        yield return new WaitForSeconds(timeDelay);

        //Turn light back on
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(minOnTime, maxOnTime);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
