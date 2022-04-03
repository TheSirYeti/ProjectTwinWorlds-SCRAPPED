using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinBreachState : IState
{
    private FloorAttack floorAttack;
    private PaladinLogic paladin;
    private Animator animator;
    private FiniteStateMachine fsm;
    private Transform target;
    private List<ShieldObject> shields;
    private bool flag = false;
    
    
    private float attackTime;
    private float currentTime;

    public PaladinBreachState(FloorAttack floorAttack, Animator animator, FiniteStateMachine fsm, Transform target, float attackTime, List<ShieldObject> shields)
    {
        this.floorAttack = floorAttack;
        this.animator = animator;
        this.fsm = fsm;
        this.target = target;
        this.attackTime = attackTime;
        this.shields = shields;
    }

    public void OnStart()
    {
        currentTime = 0f;
        DoAttack();
        foreach (ShieldObject shield in shields)
        {
            shield.SetSpeed(ShieldObject.SpeedState.FAST);
        }
        floorAttack.transform.LookAt(new Vector3(target.transform.position.x, floorAttack.transform.position.y, target.transform.position.z));
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= attackTime)
        {
            floorAttack.StopAttack();
            fsm.ChangeState(PaladinState.CHASE);
        }

        if (currentTime >= 0.8f && !flag)
        {
            flag = !flag;
            floorAttack.DoAttack();
        }
    }

    public void OnExit()
    {
        foreach (ShieldObject shield in shields)
        {
            shield.SetSpeed(ShieldObject.SpeedState.NORMAL);
        }
        StopAttack();
        paladin.attackNumber++;
    }
    
        
    public void DoAttack()
    {
        animator.SetBool("isFloorAttackFinished", false);
        animator.Play("Paladin_Stomp");
    }

    public void StopAttack()
    {
        animator.SetBool("isFloorAttackFinished", true);
    }
}
