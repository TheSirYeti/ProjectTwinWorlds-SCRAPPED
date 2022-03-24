using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] float cooldownTimer;

    private Action attackDelegate;

    private void Start()
    {
        attackDelegate = GenerateBasicAttack;
    }

    private void Update()
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
            cooldownTimer = attackCooldown ;
            Debug.Log("ATAQUE");
        }
    }
}
