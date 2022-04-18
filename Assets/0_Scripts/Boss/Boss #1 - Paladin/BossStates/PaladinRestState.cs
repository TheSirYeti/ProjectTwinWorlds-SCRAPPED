using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinRestState : IState
{
    private FiniteStateMachine _fsm;
    private Animator _animator;
    private float timeToRest;
    private float currentTime;
    private List<ShieldObject> shields;
    private PaladinLogic paladin;

    public PaladinRestState(FiniteStateMachine fsm, Animator animator, float timeToRest, List<ShieldObject> shields, PaladinLogic paladin)
    {
        _fsm = fsm;
        _animator = animator;
        this.timeToRest = timeToRest;
        this.shields = shields;
        this.paladin = paladin;
    }

    public void OnStart()
    {
        currentTime = 0f;
        _animator.SetBool("isResting", true);
        _animator.Play("Paladin_Rest");
        
        EventManager.Trigger("OnBossResting");
        
        foreach (var shield in shields)
        {
            shield.SetSpeed(ShieldObject.SpeedState.SLOW);
            Debug.Log(shield.currentSpeed);
        }
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime >= timeToRest)
        {
            _fsm.ChangeState(FSM_State.PALADIN_CHASE);
        }
    }

    public void OnExit()
    {

        _animator.SetBool("isResting", false);
        EventManager.Trigger("OnBossRestingOver");
        paladin.attackNumber = 0;
        foreach (var shield in shields)
        {
            shield.SetSpeed(ShieldObject.SpeedState.NORMAL);
            Debug.Log(shield.currentSpeed);
        }
    }
}
