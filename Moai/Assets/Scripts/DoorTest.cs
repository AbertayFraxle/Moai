using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : MonoBehaviour
{
    [SerializeField] Transform pivot;
    Vector3 position;
    private void Start()
    {
        StartCoroutine(Rotate());
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
