using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MoveToPlace : MonoBehaviour
{
    public Transform tpPoint;
    public bool objectsEnabled;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!objectsEnabled)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
                other.transform.position = tpPoint.position;
        }
        else other.transform.position = tpPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!objectsEnabled)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                collision.transform.position = tpPoint.position;
        }
        else collision.transform.position = tpPoint.position;
    }
}
