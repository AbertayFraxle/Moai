using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeChecker : MonoBehaviour
{

    bool safe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Safe")
        {
            safe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Safe")
        {
            safe = false;
        }
    }

    public bool isSafe()
    {
        return safe;
    }
}
