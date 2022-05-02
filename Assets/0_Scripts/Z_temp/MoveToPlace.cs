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
            if(other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") || other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
                other.transform.position = tpPoint.position;
        }
        else other.transform.position = tpPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!objectsEnabled)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") || collision.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
                collision.transform.position = tpPoint.position;
        }
        else collision.transform.position = tpPoint.position;
    }
}
