using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Action<float,float> movementDelegate;
    [SerializeField] private Transform direction;

    private void Start()
    {
        movementDelegate = GenerateMovement;
    }

    private void Update()
    {
        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");
        if (hMov != 0 || vMov != 0)
        {
            movementDelegate(hMov, vMov);
        }
    }

    void GenerateMovement(float h, float v)
    {
        Vector3 movement = h * direction.right + v * direction.forward;
        transform.forward = movement;
        transform.position += transform.forward * speed * Time.fixedDeltaTime;
        
    }
}
