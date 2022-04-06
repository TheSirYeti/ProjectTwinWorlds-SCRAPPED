using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISubscriber
{
    public float speed;
    public Action<float,float> movementDelegate;
    [SerializeField] private Transform direction;
    public Observer playerObserver;
    public Rigidbody rb;
    public bool canMove;

    public bool canGrapple;
    public Transform grappable;
    
    private void Start()
    {
        EventManager.Subscribe("OnSwingStart", StopMovement);
        EventManager.Subscribe("OnSwingStop", ResumeMovement);
        
        movementDelegate = GenerateMovement;
        playerObserver.Subscribe(this);
    }

    private void Update()
    {
        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");

        if (hMov != 0 || vMov != 0)
        {
            movementDelegate(hMov, vMov);
            playerObserver.NotifySubscribers("Walking");
        } else playerObserver.NotifySubscribers("Idle");

        if (Input.GetKeyDown(KeyCode.Space) && canGrapple)
        {
            transform.position = grappable.position;
            grappable = null;
        }
    }

    void GenerateMovement(float h, float v)
    {
        Vector3 movement = h * direction.right + v * direction.forward;
        
        if(movement.magnitude > 1)
            movement.Normalize();
        
        transform.forward = movement;
        //rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        rb.velocity = movement * speed * Time.deltaTime;
    }

    void NoMovement(float h, float v)
    {
        
    }

    public void OnNotify(string eventID)
    {
        if (eventID == "BasicAttack")
        {
            movementDelegate = NoMovement;
        }

        if (eventID == "NoAttack")
        {
            movementDelegate = GenerateMovement;
        }
    }

    void StopMovement(object[] parameters)
    {
        movementDelegate = NoMovement;
        rb.useGravity = false;
    }

    void ResumeMovement(object[] parameters)
    {
        movementDelegate = GenerateMovement;
        rb.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            grappable = other.gameObject.GetComponent<MovableObject>().grappablePoint;
            canGrapple = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            grappable = null;
            canGrapple = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            Debug.Log("VELOCITY 0 PA");
        }
    }
}