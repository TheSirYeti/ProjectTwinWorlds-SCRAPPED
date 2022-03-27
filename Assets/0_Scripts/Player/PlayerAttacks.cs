using System;
using System.Collections;
using System.Collections.Generic;
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
    
    private void Start()
    {
        attackDelegate = GenerateBasicAttack;
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
            AimAbility();
        }
    }

    public abstract void GenerateBasicAttack();

    void StopAttacking()
    {
        _playerObserver.NotifySubscribers("NoAttack");
    }

    public abstract void AimAbility();

    public abstract void ThrowAbility();
}
