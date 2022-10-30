using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    public bool isOpen;
    [SerializeField] Transform pivot;
    [SerializeField] float openTime;
    Vector3 position;

    public void ChangeDoorState()
    {
        StopAllCoroutines();
        if (isOpen)
        {
            isOpen = false;
            StartCoroutine(CloseDoor());
        }
        else
        {
            isOpen = true;
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        float timer = 0;
        while (timer < openTime)
        {
            pivot.transform.localRotation = Quaternion.Lerp(pivot.transform.localRotation, Quaternion.Euler(0, -90, 0), timer / openTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CloseDoor()
    {
        float timer = 0;
        while (timer < openTime)
        {
            pivot.transform.localRotation = Quaternion.Lerp(pivot.transform.localRotation, Quaternion.identity, timer / openTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Rotate()
    {
        position = transform.position;
        // pivot.localRotation = Quaternion.Euler(0, -90, 0);
        //transform.localEulerAngles = transform.localEulerAngles +
        //     new Vector3(0, 90, 0);
        //transform.position = position;
        pivot.transform.Rotate(Vector3.up * -90);
        yield return null;
    }
}
