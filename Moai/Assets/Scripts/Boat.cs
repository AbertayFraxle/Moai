using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Obstacle
{
    bool hasFuel;
    public bool isUnlocked;
    public override void UseItem()
    {
        if (!hasFuel)
        {
            hasFuel = true;
            itemRequired = "Boat Key";
        }
        else if (!isUnlocked)
        {
            isUnlocked = true;
        }

    }

}
