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
        other.transform.position = tpPoint.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = tpPoint.position;
    }
}
