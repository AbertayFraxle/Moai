using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveHud : MonoBehaviour
{
    public float spawnDistance = 2;
    public GameObject HUD;
    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUD.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        HUD.transform.LookAt(new Vector3(head.position.x, HUD.transform.position.y, head.position.z));
        HUD.transform.forward *= -1;
    }
}
