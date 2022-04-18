using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSummonState : MonoBehaviour, IState
{
    private PaladinLogic paladin;
    private FiniteStateMachine fsm;
    
    private float currentTime;
    private float attackTime;
    private float timeSummoning;
    private float summonAmount;

    public PaladinSummonState(PaladinLogic paladin, FiniteStateMachine fsm, float timeSummoning, float summonAmount)
    {
        this.paladin = paladin;
        this.fsm = fsm;
        this.timeSummoning = timeSummoning;
        this.summonAmount = summonAmount;
    }

    public void OnStart()
    {
        paladin.animator.SetBool("isSummoning", true);
        paladin.animator.Play("Paladin_Summon");
        paladin.SetShieldSpeeds(ShieldObject.SpeedState.FAST);
        currentTime = 0f;
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime < timeSummoning)
        {
            attackTime += Time.deltaTime;
            if (attackTime >=  summonAmount)
            {
                paladin.SummonDropShield();
                attackTime = 0f;
            }
        }
        else
        {
            fsm.ChangeState(FSM_State.PALADIN_CHASE);
        }
    }

    public void OnExit()
    {
        paladin.animator.SetBool("isSummoning", false);
        paladin.attackNumber++;
        paladin.SetShieldSpeeds(ShieldObject.SpeedState.NORMAL);
    }
}
