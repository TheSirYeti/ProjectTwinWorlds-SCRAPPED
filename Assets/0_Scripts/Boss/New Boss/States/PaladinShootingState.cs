using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinShootingState : IState
{
    private FiniteStateMachine fsm;
    private PaladinBoss paladin;
    private Animator animator;
    private int attackNumber, originalNumber;
    private float attackTime, startTime;
    private Transform startPoint, target, centerPosition, topPosition;
    private GameObject bulletPrefab;

    private bool isShooting;
    private float attackCooldown;
    private float startCooldown;

    private ParticleSystem poof1, poof2;

    public PaladinShootingState(FiniteStateMachine fsm, PaladinBoss paladin, Animator animator, int attackNumber, float attackTime, float startTime, Transform startPoint, Transform target, Transform centerPosition, Transform topPosition, GameObject bulletPrefab, ParticleSystem poof1, ParticleSystem poof2)
    {
        this.fsm = fsm;
        this.paladin = paladin;
        this.animator = animator;
        this.originalNumber = attackNumber;
        this.attackTime = attackTime;
        this.startTime = startTime;
        this.startPoint = startPoint;
        this.target = target;
        this.centerPosition = centerPosition;
        this.topPosition = topPosition;
        this.bulletPrefab = bulletPrefab;
        this.poof1 = poof1;
        this.poof2 = poof2;
    }

    public void OnStart()
    {
        target = paladin.target;
        EventManager.UnSubscribe("OnPlayerChange", ChangeTarget);
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        
        isShooting = false;
        poof1.transform.position = centerPosition.position;
        poof2.transform.position = topPosition.transform.position;
        
        poof1.Emit(100);
        poof2.Emit(100);

        paladin.transform.position = topPosition.position;
        
        attackNumber = originalNumber;
        animator.Play("Paladin_Casting");
        animator.SetBool("isSummoning", true);
        attackCooldown = 0;
        startCooldown = startTime + Time.deltaTime;
    }

    public void OnUpdate()
    {
        startCooldown -= Time.deltaTime;
        attackCooldown -= Time.deltaTime;
        
        if (startCooldown <= 0 && !isShooting)
        { 
            isShooting = true;
            startCooldown = Mathf.Infinity;
            return;
        }

        if (isShooting && attackCooldown <= 0)
        {
            attackNumber--;

            GameObject bullet = paladin.InstantiateBullet(bulletPrefab);
            bullet.transform.position = startPoint.position;
            bullet.transform.LookAt(target);
            
            if (attackNumber <= 0)
            {
                isShooting = false;
                fsm.ChangeState(FSM_State.PALADIN_SUMMON);
                attackCooldown = Mathf.Infinity;
            }

            attackCooldown = attackTime + Time.deltaTime;
        }
    }

    public void OnExit()
    {
        animator.SetBool("isSummoning", false);
        poof1.transform.position = centerPosition.position;
        poof2.transform.position = topPosition.transform.position;
        
        poof1.Emit(100);
        poof2.Emit(100);

        paladin.transform.position = centerPosition.position;
    }
    
    void ChangeTarget(object[] parameters)
    {
        Debug.Log("cambio");
        paladin.isDemon = !paladin.isDemon;
        
        if (paladin.isDemon)
        {
            target = paladin.angel;
        }
        else target = paladin.demon;
    }
}
