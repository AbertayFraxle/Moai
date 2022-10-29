using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] float changeTime = 20;
    public void ChangeDoorState()
    {
        StartCoroutine(AnimateDoor());
        if (isOpen)
        {

        }
    }

    private IEnumerator AnimateDoor()
    {
        float timer = 0;
        while (timer < changeTime)
        {
            //transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0, 90, timer / changeTime), 0);
            // Vector3 newRotation = new Vector3(0, transform.rotation.y + 1, 0);
            // transform.eulerAngles = newRotation;
            // //transform.localRotation += Quaternion.Euler(0, 1, 0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 10, 0)); 
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
