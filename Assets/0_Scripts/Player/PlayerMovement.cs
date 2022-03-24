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
        movementDelegate(hMov, vMov);
    }

    void GenerateMovement(float h, float v)
    {
        Vector3 movement = h * direction.right + v * direction.forward;
        transform.position += movement * speed * Time.fixedDeltaTime;
    }
}
