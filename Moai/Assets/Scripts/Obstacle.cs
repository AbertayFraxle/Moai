using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public string itemRequired;

    public void UseItem()
    {
        Destroy(gameObject);
    }
}
