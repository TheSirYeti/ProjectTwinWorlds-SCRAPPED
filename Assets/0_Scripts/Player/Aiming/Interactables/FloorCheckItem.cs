using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheckItem : MonoBehaviour
{
    public MovableItem myItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            myItem.isFalling = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            myItem.isFalling = true;
        }
    }
}
