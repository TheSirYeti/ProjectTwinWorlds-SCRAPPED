using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinTackleState : IState
{
    private float timeToCharge;
    private float timeToNextState;
    private float currentTime;

    private bool isTackling;

    private FiniteStateMachine fsm;
    private Animator animator;
    private PaladinLogic paladin;
    
    public void OnStart()
    {
        currentTime = 0;
        animator.Play("Paladin_Idle");
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToCharge && !isTackling)
        {
            animator.SetTrigger("onTackle");
            isTackling = true;
        }

        if (currentTime >= timeToNextState)
        {
            fsm.ChangeState(PaladinState.CHASE);
        }
    }

    public void OnExit()
    {
        paladin.attackNumber++;
    }
}
