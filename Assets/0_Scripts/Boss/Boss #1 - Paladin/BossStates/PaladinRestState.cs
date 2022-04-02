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

    public PaladinRestState(FiniteStateMachine fsm, Animator animator, float timeToRest, List<ShieldObject> shields)
    {
        _fsm = fsm;
        _animator = animator;
        this.timeToRest = timeToRest;
        this.shields = shields;
    }

    public void OnStart()
    {
        currentTime = 0f;
        _animator.SetBool("isResting", true);
        _animator.Play("Paladin_Rest");
        
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
            Debug.Log("change");
            _fsm.ChangeState(PaladinState.CHASE);
        }
    }

    public void OnExit()
    {
        _animator.SetBool("isResting", false);
        foreach (var shield in shields)
        {
            shield.SetSpeed(ShieldObject.SpeedState.NORMAL);
            Debug.Log(shield.currentSpeed);
        }
    }
}
