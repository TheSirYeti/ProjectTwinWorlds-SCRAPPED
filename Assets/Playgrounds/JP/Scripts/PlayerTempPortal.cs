using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempPortal : MonoBehaviour
{
    public Transform tpPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.Trigger("TPPlayers", tpPoint.position);
        }
    }
}
