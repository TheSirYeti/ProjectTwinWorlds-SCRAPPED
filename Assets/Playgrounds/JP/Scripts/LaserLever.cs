using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLever : MonoBehaviour
{
    [SerializeField] private GameObject leverOn, leverOff;
    [SerializeField] private bool status;
    [SerializeField] private LaserLogic myLaser;
    [SerializeField] private float minDistance;
    private bool canInteract = false; 
    private GameObject currentPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && CheckInteractStatus())
        {
            status = !status;
            myLaser.SetLaserStatus(status);

            if (status)
            {
                leverOff.SetActive(false);
                leverOn.SetActive(true);
            }
            else
            {
                leverOff.SetActive(true);
                leverOn.SetActive(false);
            }
        }
    }

    bool CheckInteractStatus()
    {
        if (!canInteract)
            return false;

        if (currentPlayer != null 
            && Vector3.Distance(currentPlayer.transform.position, transform.position) <= minDistance
            && currentPlayer.activeSelf)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canInteract = true;
            currentPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && currentPlayer == other.gameObject)
        {
            canInteract = false;
            currentPlayer = null;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && currentPlayer == other.gameObject)
        {
            canInteract = false;
            currentPlayer = null;
        }
    }
}
