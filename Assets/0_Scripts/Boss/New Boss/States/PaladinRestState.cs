using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PaladinRestState : IState
{
    private FiniteStateMachine fsm;
    private PaladinBoss paladin;
    private Animator animator;
    private float waitTime, waitCooldown;

    public PaladinRestState(FiniteStateMachine fsm, PaladinBoss paladin, Animator animator, float waitTime)
    {
        this.fsm = fsm;
        this.paladin = paladin;
        this.animator = animator;
        this.waitTime = waitTime;
    }

    public void OnStart()
    {
        animator.Play("Paladin_Rest");
        animator.SetBool("isResting", true);
        waitCooldown = waitTime;
    }

    public void OnUpdate()
    {
        waitCooldown -= Time.deltaTime;
        if (waitCooldown <= 0)
        {
            fsm.ChangeState(FSM_State.PALADIN_TACKLE);
        }
    }

    public void OnExit()
    {
        animator.SetBool("isResting", false);
    }
}
