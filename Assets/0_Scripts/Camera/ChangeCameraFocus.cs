using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeCameraFocus : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public GameObject newPos;
    public bool onlyDemon;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyDemon)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && other.gameObject.activeSelf)
            {
                vCam.Follow = newPos.transform;
            }
        }
        else
        {
            if ((other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && other.gameObject.activeSelf)
                || (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && other.gameObject.activeSelf))
            {
                vCam.Follow = newPos.transform;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (onlyDemon)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && other.gameObject.activeSelf)
            {
                vCam.Follow = newPos.transform;
            }
        }
        else
        {
            if ((other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && other.gameObject.activeSelf)
                || (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && other.gameObject.activeSelf))
            {
                vCam.Follow = newPos.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && other.gameObject.activeSelf)
            || (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && other.gameObject.activeSelf))
        {
            vCam.Follow = PlayerWorlds.instance.currentPlayer.transform;
        }
    }
}
