using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TempElevator : MonoBehaviour
{
    public List<Transform> elevatorPoints;
    public Transform elevator;
    public float speed, minDistance;

    private bool isStalling;
    private int currentWaypoint;
    private void Update()
    {
        if (!isStalling)
        {
            Vector3 distance = (elevatorPoints[currentWaypoint].position - elevator.position).normalized;
            elevator.position += distance * speed * Time.deltaTime;

            if (Vector3.Distance(elevator.position, elevatorPoints[currentWaypoint].position) <= minDistance)
            {
                StartCoroutine(Buffer());
            }
        }
    }

    public IEnumerator Buffer()
    {
        isStalling = true;
        yield return new WaitForSeconds(2f);
        ChangeWaypoint();
        isStalling = false;
        yield return new WaitForSeconds(0.01f);
    }

    void ChangeWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= elevatorPoints.Count)
        {
            elevatorPoints.Reverse();
            currentWaypoint = 0;
        }
    }
}
