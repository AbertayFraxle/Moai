using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : Obstacle
{
    public override void UseItem()
    {
        gameObject.tag = "Door";
    }

}
