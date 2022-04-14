using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ISubscriber
{
    public float speed, swingForce;
    public Action<float,float> movementDelegate;
    [SerializeField] private Transform direction;
    public Observer playerObserver;
    public Rigidbody rb, currentSwing;
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
        
        
        movementDelegate = GenerateMovement;
        playerObserver.Subscribe(this);
    }

    private void FixedUpdate()
    {
        float hMov = Input.GetAxis("Horizontal");
        float vMov = Input.GetAxis("Vertical");
        
        
        if (new Vector3(hMov, 0, vMov) != Vector3.zero)
        {
            movementDelegate(hMov, vMov);
            playerObserver.NotifySubscribers("Walking");
        } else playerObserver.NotifySubscribers("Idle");

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transform.position = jumpSpot.position;
            jumpSpot = null;
            canJump = false;
        }
    }

    public void GenerateMovement(float h, float v)
    {
        Vector3 movement = new Vector3(h, 0, v);

        if (movement.magnitude >= 1)
        {
            movement.Normalize();
        }
        
        Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        Vector3 rotatedInput = matrix.MultiplyPoint3x4(movement);
        Vector3 relativeDistance = (transform.position + rotatedInput) - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativeDistance, Vector3.up);

        transform.rotation = rotation;

        if (canMove)
        {
            rb.velocity = new Vector3(rotatedInput.x * speed * Time.deltaTime, rb.velocity.y, rotatedInput.z * speed * Time.deltaTime);
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
            movement = new Vector3(v, 0, h);
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
            
            currentSwing.AddForce(movement * swingForce, ForceMode.Acceleration);
            transform.position = transform.parent.position;
        }
    }

    public void ClimbMovement(float h, float v)
    {
        Vector3 movement;
        
        if(isClimbLeft)
            movement = new Vector3(h, v, 0);
        else
            movement = new Vector3(0, v, h);

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
            movementObject = new Vector3(0, v, 0);
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
            currentSwing = (Rigidbody) parameters[0];
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
            //rb.velocity = currentSwing.velocity;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            jumpSpot = other.gameObject.GetComponent<MovableItem>().jumpSpot;
            canJump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
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