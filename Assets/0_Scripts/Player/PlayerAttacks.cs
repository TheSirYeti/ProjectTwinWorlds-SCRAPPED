using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerAttacks : MonoBehaviour
{
    public float attackCooldown;
    public float cooldownTimer;
    
    public Observer _playerObserver;
    public Action attackDelegate;

    public float distance, radius;
    public Camera cam;
    public LayerMask wallLayer;
    public bool usedAbility;
    public LayerMask collidableLayer;
    
    
    private void Start()
    {
        attackDelegate = GenerateBasicAttack;
        EventManager.Subscribe("ResetAbility", ThrowAbility);
    }

    private void Update()
    {
        cooldownTimer -= Time.fixedDeltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer <= 0)
        {
            attackDelegate();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (usedAbility)
            {
                CheckAbility();
            }
            else
            {
                ThrowAbility(null);
            }
            usedAbility = !usedAbility;
        }
    }

    public abstract void GenerateBasicAttack();

    void StopAttacking()
    {
        _playerObserver.NotifySubscribers("NoAttack");
    }

    public abstract void AimAbility(Vector3 destination);

    public void CheckAbility()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, collidableLayer))
        {
            CheckForCollisions(hit.point);
        }

    }

    public void CheckForCollisions(Vector3 objectPosition)
    {
        Collider[] collisions = Physics.OverlapSphere(objectPosition, radius, collidableLayer);

        if (collisions.Length == 1)
        {
            InteractableObject intObj = collisions[0].GetComponent<InteractableObject>();

            var dirToTarget = objectPosition - transform.position;
            
            if (LayerMask.NameToLayer(intObj.layerTrigger) == gameObject.layer && intObj.CheckForTrigger() && !Physics.Raycast(transform.position, dirToTarget, dirToTarget.magnitude, wallLayer))
            {
                intObj.OnObjectStart();
                
                AimAbility(intObj.insertionPoint.position);
            }
            else
            {
                if (LayerMask.NameToLayer(intObj.firstTrigger) == gameObject.layer)
                {
                    AimAbility(intObj.insertionPoint.position);
                    intObj.isFirstTriggered = true;
                }
            }
        }
    }
    
    public abstract void ThrowAbility(object[] parameters);
}
