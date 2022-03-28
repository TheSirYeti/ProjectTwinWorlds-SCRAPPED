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

    public float distance;
    public Camera cam;
    public LayerMask wallLayer;
    public bool usedAbility;
    
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
                AimAbility();
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

    public abstract void AimAbility();

    public abstract void ThrowAbility(object[] parameters);
}
