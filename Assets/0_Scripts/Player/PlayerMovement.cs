using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISubscriber
{
    public float speed, acceleration, maxSpeed, swingForce;
    public Action<float,float> movementDelegate;
    public CinemachineVirtualCamera camera;
    public Observer playerObserver;
    public Rigidbody rb;
    public SwingPhysics currentSwing;
    public bool canMove;

    public Transform myParent;
    public bool isDemon;
    public bool isSwingLeft, isClimbLeft, isPulleyLeft;

    public bool canJump;
    public Transform jumpSpot;
    public Rigidbody pulleyObject;
    
    private void Start()
    {
        EventManager.Subscribe("OnSwingStart", StartSwing);
        EventManager.Subscribe("OnSwingStop", StopSwing);
        EventManager.Subscribe("OnMovableRestrict", RestrictSpeed);
        EventManager.Subscribe("OnMovableUnrestrict", UnrestrictSpeed);
        EventManager.Subscribe("OnClimbStart", StartClimb);
        EventManager.Subscribe("OnClimbStop", StopClimb);
        EventManager.Subscribe("OnPulleyStart", StartPulley);
        EventManager.Subscribe("OnPulleyStop", StopPulley);


        camera = FindObjectOfType<CinemachineVirtualCamera>();
        movementDelegate = GenerateMovement;
        playerObserver.Subscribe(this);
    }

    private void FixedUpdate()
    {
        float hMov = Input.GetAxisRaw("Horizontal");
        float vMov = Input.GetAxisRaw("Vertical");

        if (new Vector3(hMov, 0, vMov) != Vector3.zero || movementDelegate == SwingMovement)
        {
            movementDelegate(hMov, vMov);
            playerObserver.NotifySubscribers("Walking");
        }
        else
        {
            playerObserver.NotifySubscribers("Idle");
            speed = 0;

            if (movementDelegate == GenerateMovement)
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transform.position = jumpSpot.position;
            jumpSpot = null;
            canJump = false;
        }
    }

    public void GenerateMovement(float h, float v)
    {
        if(speed <= maxSpeed)
            speed += acceleration * Time.fixedDeltaTime;
        
        Vector3 movement = new Vector3(h, 0, v);

        if(movement.magnitude >= 1)
            movement.Normalize();
        
        float direction = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
        var rotGoal = Quaternion.Euler(0, direction, 0);
        Vector3 moveDir = Quaternion.Euler(0, direction, 0) * Vector3.forward;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.3f);
        
        
        if (canMove)
        {
            rb.velocity = new Vector3(moveDir.x * speed * Time.fixedDeltaTime, rb.velocity.y, moveDir.z * speed * Time.fixedDeltaTime);

            /*if (rb.velocity.magnitude >= maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }*/
        }
    }
    
    public void PostSwingMovement(float h, float v)
    {
        Vector3 movement;
        
        if (isSwingLeft)
        {
            movement = new Vector3(h, 0, v);
        }
        else
        {
            movement = new Vector3(v, 0, h * -1);
        }
        
        
        if(movement.magnitude > 1)
            movement.Normalize();
        
        transform.forward = movement;

        if (canMove)
        {
            rb.velocity = new Vector3(movement.x * speed * Time.deltaTime, rb.velocity.y, movement.z * speed * Time.deltaTime);
        }
    }

    public void NoMovement(float h, float v)
    {
        
    }

    public void SwingMovement(float h, float v)
    {
        /*
        if (currentSwing != null)
        {
            Vector3 movement;

            if (isSwingLeft)
            {
                movement = new Vector3(h, 0, 0);
            }
            else
            {
                movement = new Vector3(0, 0, h * -1);
            }
        }
        */
        
        transform.position = transform.parent.position;
    }

    public void ClimbMovement(float h, float v)
    {
        Vector3 movement;
        
        if(isClimbLeft)
            movement = new Vector3(h, v, 0);
        else
            movement = new Vector3(0, v,  -1 * h);

        rb.velocity = movement * (speed / 2) * Time.deltaTime;
    }

    public void PulleyMovement(float h, float v)
    {
        Vector3 movementPlayer, movementObject;

        if (isPulleyLeft)
        {
            movementPlayer = new Vector3(v, 0, 0);
            movementObject = new Vector3(0, v, 0);
        }
        else
        {
            movementPlayer = new Vector3(0, 0, v);
            movementObject = new Vector3(0, v * -1, 0);
        }
        
        rb.velocity = movementPlayer * (speed / 5f) * Time.deltaTime;
        pulleyObject.velocity = movementObject * (speed / 5f) * Time.deltaTime;
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

    void StartSwing(object[] parameters)
    {
        if (!isDemon)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            currentSwing = (SwingPhysics) parameters[0];
            Transform newPos = (Transform) parameters[1];
            isSwingLeft = (bool) parameters[2];
            transform.position = newPos.position;
            transform.SetParent(newPos);
            movementDelegate = SwingMovement;
        }
    }
    
    void StopSwing(object[] parameters)
    {
        if (!isDemon)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            if (currentSwing != null)
            {
                if(currentSwing.GetClosestPoint(transform) == currentSwing.leftPoint)
                    rb.AddForce(currentSwing.transform.right * 15f, ForceMode.Impulse);
                else rb.AddForce(currentSwing.transform.right * -15f, ForceMode.Impulse);
            }
            
            currentSwing = null;
            transform.SetParent(myParent);
            movementDelegate = PostSwingMovement;
        }
    }

    void StartClimb(object[] parameters)
    {
        if (!isDemon)
        {
            rb.useGravity = false;
            isClimbLeft = (bool) parameters[0];
            movementDelegate = ClimbMovement;
        }
    }
    
    void StopClimb(object[] parameters)
    {
        if (!isDemon)
        {
            rb.useGravity = true;
            movementDelegate = GenerateMovement;
        }
    }

    void StartPulley(object[] parameters)
    {
        if (isDemon)
        {
            pulleyObject = (Rigidbody) parameters[0];
            pulleyObject.useGravity = false;
            pulleyObject.velocity = Vector3.zero;
            movementDelegate = PulleyMovement;
        }
    }
    
    void StopPulley(object[] parameters)
    {
        if (isDemon && pulleyObject != null)
        {
            pulleyObject.useGravity = true;
            pulleyObject.velocity = Vector3.zero;
            pulleyObject = null;
            movementDelegate = GenerateMovement;
        }
    }
    
    private void RestrictSpeed(object[] parameters)
    {
        canMove = false;
    }

    private void UnrestrictSpeed(object[] parameters)
    {
        canMove = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && isDemon)
        {
            jumpSpot = other.gameObject.GetComponent<MovableItem>().jumpSpot;
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object") && isDemon)
        {
            jumpSpot = null;
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        
        
    }
}