using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorTrigger : MonoBehaviour
{
    public PlayerMovement angelMovement, demonMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (angelMovement.movementDelegate == angelMovement.PostSwingMovement)
            {
                angelMovement.movementDelegate = angelMovement.GenerateMovement;
            }
        }
    }
}
