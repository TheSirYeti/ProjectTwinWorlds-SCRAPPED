using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MovingSpade : MonoBehaviour
{
    public Transform objectToMove;

    public List<Transform> waypoints;

    public float speed, minDistance;
    private int currentWaypoint = 0;
    
    private void Update()
    {
        Vector3 direction = waypoints[currentWaypoint].position - objectToMove.position;
        direction.Normalize();
        objectToMove.transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(objectToMove.position, waypoints[currentWaypoint].position) <= minDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            Debug.Log("sexo");
            EventManager.Trigger("ResetAbility", PlayerWorlds.instance.angelPlayer);
        }
    }
}
