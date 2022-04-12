using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MoveToPlace : MonoBehaviour
{
    public Transform tpPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") || other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
            other.transform.position = tpPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") || collision.gameObject.layer == LayerMask.NameToLayer("DemonPlayer"))
            collision.transform.position = tpPoint.position;
    }
}
