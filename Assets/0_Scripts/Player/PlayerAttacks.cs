using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float cooldownTimer;
    
    public Observer _playerObserver;
    private Action attackDelegate;

    private void Start()
    {
        attackDelegate = GenerateBasicAttack;
    }

    private void FixedUpdate()
    {
        cooldownTimer -= Time.fixedDeltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackDelegate();
        }
    }
 
    void GenerateBasicAttack()
    {
        if (cooldownTimer <= 0)
        {
            cooldownTimer = attackCooldown;
            Debug.Log("ATAQUE");
            _playerObserver.NotifySubscribers("BasicAttack");
        }
    }

    void StopAttacking()
    {
        _playerObserver.NotifySubscribers("NoAttack");
    }
}
