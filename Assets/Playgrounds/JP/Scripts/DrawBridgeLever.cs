using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridgeLever : MonoBehaviour
{
    public Animator myBridge;
    public bool canInteract;
    public float minDistance;
    public GameObject flickOn, flickOff;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canInteract && !flickOff.activeSelf && HasNearbyPlayer())
        {
            myBridge.Play("BridgeFall");
            flickOff.SetActive(true);
            flickOn.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") || other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            canInteract = false;
        }
    }

    bool HasNearbyPlayer()
    {
        Debug.Log(Vector3.Distance(transform.position, PlayerWorlds.instance.demonPlayer.transform.position));
        Debug.Log(Vector3.Distance(transform.position, PlayerWorlds.instance.angelPlayer.transform.position));
        
        if (PlayerWorlds.instance.demonPlayer.activeSelf)
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.demonPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerWorlds.instance.angelPlayer.transform.position) <=
                minDistance)
            {
                return true;
            }
        }

        return false;
    }
}
