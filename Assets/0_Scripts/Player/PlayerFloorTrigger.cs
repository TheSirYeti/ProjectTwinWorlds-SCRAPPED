using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerFloorTrigger : MonoBehaviour
{
    public PlayerMovement angelMovement, demonMovement;
    public Transform reference;

    private void Update()
    {
        transform.position = reference.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor") || other.gameObject.layer == LayerMask.NameToLayer("Breakable Object"))
        {
            if (angelMovement.movementDelegate == angelMovement.PostSwingMovement)
            {
                angelMovement.movementDelegate = angelMovement.GenerateMovement;
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor")  || other.gameObject.layer == LayerMask.NameToLayer("Breakable Object"))
        {
            if (angelMovement.movementDelegate == angelMovement.PostSwingMovement)
            {
                angelMovement.movementDelegate = angelMovement.GenerateMovement;
            }
        }
    }
}
