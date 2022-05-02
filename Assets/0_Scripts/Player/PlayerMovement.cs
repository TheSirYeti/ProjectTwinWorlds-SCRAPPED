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
    public PulleySystem currentPulley;
    public bool canMove;
    private float previousDistance;
    
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

        if (new Vector3(hMov, 0, vMov) != Vector3.zero)
        {
            movementDelegate(hMov, vMov);
            playerObserver.NotifySubscribers("Walking");
        }
        else
        {
            playerObserver.NotifySubscribers("Idle");
            speed = 0;
            
            CheckIdleStatus();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            transform.position = jumpSpot.position;
            jumpSpot = null;
            canJump = false;
            EventManager.Trigger("ResetAbility");
        }
    }

    void CheckIdleStatus()
    {
        if (movementDelegate == GenerateMovement)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            
        else if (movementDelegate == PulleyMovement)
            pulleyObject.velocity = Vector3.zero;
        
        else if (movementDelegate != PostSwingMovement)
        {
            rb.velocity = Vector3.zero;
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
        }
    }
    
    public void PostSwingMovement(float h, float v)
    {
        /*Vector3 movement;
        
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
            rb.velocity = new Vector3(movement.x * maxSpeed * Time.deltaTime, rb.velocity.y, movement.z * speed * Time.deltaTime);
        }*/
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

        rb.velocity = movement * (maxSpeed / 4) * Time.deltaTime;
    }

    public void PulleyMovement(float h, float v)
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
        
        if (canMove && movement != Vector3.zero)
        {
            rb.velocity = new Vector3(moveDir.x * (speed / 4f) * Time.fixedDeltaTime, rb.velocity.y, moveDir.z * (speed / 4f) * Time.fixedDeltaTime);
        }
        
        float currentDistance = Vector3.Distance(transform.position, currentPulley.transform.position);
        if (previousDistance < currentDistance)
        {
            pulleyObject.velocity = new Vector3(0, (speed / 4f) * Time.fixedDeltaTime, 0);
        }
        else if (previousDistance > currentDistance)
        {
                pulleyObject.velocity = new Vector3(0, ((speed / 4f) * Time.fixedDeltaTime) * -1, 0);
        }
        

        Debug.Log(previousDistance + " | " + currentDistance);
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
            currentPulley = (PulleySystem) parameters[1];
            pulleyObject.useGravity = false;
            pulleyObject.velocity = Vector3.zero;
            movementDelegate = PulleyMovement;
            StartCoroutine(SavePreviousPosition());
        }
    }
    
    void StopPulley(object[] parameters)
    {
        if (isDemon && pulleyObject != null)
        {
            pulleyObject.useGravity = true;
            pulleyObject.velocity = Vector3.zero;
            pulleyObject = null;
            currentPulley = null;
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

    IEnumerator SavePreviousPosition()
    {
        while (pulleyObject != null && currentPulley != null)
        {
            previousDistance = Vector3.Distance(transform.position, currentPulley.transform.position);
            yield return new WaitForSeconds(0.03f);
        }
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