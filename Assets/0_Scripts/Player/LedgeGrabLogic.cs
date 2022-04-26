using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabLogic : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LedgePoint"))
        {
            playerMovement.canJump = true;
            playerMovement.jumpSpot = other.GetComponent<BoxHeightLogic>().point;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LedgePoint"))
        {
            playerMovement.canJump = true;
            playerMovement.jumpSpot = other.GetComponent<BoxHeightLogic>().point;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LedgePoint"))
        {
            playerMovement.canJump = false;
            playerMovement.jumpSpot = null;
        }
    }
}
