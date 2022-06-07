using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PaladinSummonState : IState
{
    private FiniteStateMachine fsm;
    private PaladinBoss paladin;
    private Animator animator;
    private SwordSummoning swordSummoning;
    private int amountOfSwords, loopAmount;
    private float waitTime, waitCooldown;

    public PaladinSummonState(FiniteStateMachine fsm, PaladinBoss paladin, Animator animator, SwordSummoning swordSummoning, int amountOfSwords, int loopAmount, float waitTime)
    {
        this.fsm = fsm;
        this.paladin = paladin;
        this.animator = animator;
        this.swordSummoning = swordSummoning;
        this.amountOfSwords = amountOfSwords;
        this.loopAmount = loopAmount;
        this.waitTime = waitTime;
    }

    public void OnStart()
    {

        animator.Play("Paladin_Summon");
        swordSummoning.DoSwordElevation(amountOfSwords, loopAmount);
        waitCooldown = waitTime;
    }

    public void OnUpdate()
    {
        waitCooldown -= Time.deltaTime;
        if (waitCooldown <= 0)
        {
            fsm.ChangeState(FSM_State.PALADIN_REST);
        }
    }

    public void OnExit()
    {
        //
    }
    
    
}
