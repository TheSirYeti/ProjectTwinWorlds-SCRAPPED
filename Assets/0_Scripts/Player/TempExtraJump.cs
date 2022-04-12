using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TempExtraJump : MonoBehaviour
{
    public Transform nextPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            other.GetComponent<PlayerMovement>().canJump = true;
            other.GetComponent<PlayerMovement>().jumpSpot = nextPos;
            Debug.Log("tuqi");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            other.GetComponent<PlayerMovement>().canJump = true;
            other.GetComponent<PlayerMovement>().jumpSpot = nextPos;
            Debug.Log("tuqi");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") ||
            other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            other.GetComponent<PlayerMovement>().canJump = false;
            other.GetComponent<PlayerMovement>().jumpSpot = null;
            Debug.Log("tuqi");
        }
    }
}
