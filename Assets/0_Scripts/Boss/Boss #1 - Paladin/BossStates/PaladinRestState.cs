using Unity.VisualScripting;
using UnityEngine;

public class PaladinRestState : IState
{
    private FiniteStateMachine _fsm;
    private Animator _animator;
    private float timeToRest;
    private float currentTime;

    public PaladinRestState(FiniteStateMachine fsm, Animator animator, float timeToRest)
    {
        _fsm = fsm;
        _animator = animator;
        this.timeToRest = timeToRest;
    }

    public void OnStart()
    {
        currentTime = 0f;
        _animator.Play("Paladin_Rest");
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToRest)
        {
            _fsm.ChangeState(PaladinState.CHASE);
        }
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
