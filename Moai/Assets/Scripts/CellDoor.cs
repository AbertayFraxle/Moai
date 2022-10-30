using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : Obstacle
{
    bool isUnlocked = false;
    public override void UseItem()
    {
        isUnlocked = true;
        gameObject.tag = "Door";
    }

}
