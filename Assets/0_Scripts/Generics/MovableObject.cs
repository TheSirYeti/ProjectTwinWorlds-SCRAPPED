using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public bool isActive;
    public float minDistance;
    public float speed;

    public int id;
    
    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        EventManager.Subscribe("OnMovableCollided", EnableFollow);
        EventManager.Subscribe("ResetAbility", DisableFollow);
    }

    private void Update()
    {
        if (isActive && Vector3.Distance(transform.position, target.position) > minDistance)
        {
            Vector3 direction = transform.position - target.position;
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    public void ChangeTarget(object[] parameters)
    {
        GameObject newTarget = (GameObject)parameters[0];
        target = newTarget.transform;
    }

    public void EnableFollow(object[] parameters)
    {
        if ((int) parameters[0] == id) 
            isActive = true;
    }
    
    public void DisableFollow(object[] parameters)
    {
        isActive = false;
    }
}
