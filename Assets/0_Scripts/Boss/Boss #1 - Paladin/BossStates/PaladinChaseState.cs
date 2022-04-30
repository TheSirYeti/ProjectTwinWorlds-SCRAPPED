using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaladinChaseState : IState
{
    public Transform target;
    public PaladinLogic paladin;
    private FiniteStateMachine _fsm;
    private Animator _animator;
    private float speed;
    private float minDistance;


    public PaladinChaseState(Transform target, PaladinLogic paladin, FiniteStateMachine fsm, Animator animator, float speed, float minDistance)
    {
        this.target = target;
        this.paladin = paladin;
        _fsm = fsm;
        _animator = animator;
        this.speed = speed;
        this.minDistance = minDistance;
    }

    public void OnStart()
    {
        EventManager.UnSubscribe("OnPlayerChange", ChangeTarget);
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        
        target = paladin.target;
        
        if (paladin.attackNumber >= paladin.maxAttackNumber)
        {
            _fsm.ChangeState(FSM_State.PALADIN_RETURN);
        }
        
        _animator.SetBool("isWalking", true);
        _animator.Play("Paladin_Walking");
    }

    public void OnUpdate()
    {
        Vector3 direction = target.transform.position - paladin.transform.position;
        direction.Normalize();
        paladin.transform.position += direction * speed * Time.deltaTime;
        paladin.transform.LookAt(new Vector3(target.transform.position.x, paladin.transform.position.y, target.transform.position.z));
        if (Vector3.Distance(target.transform.position, paladin.transform.position) <= minDistance)
        {
            ChooseAttack();
        }
    }

    public void OnExit()
    {
        _animator.SetBool("isWalking", false);
    }

    void ChooseAttack()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                _fsm.ChangeState(FSM_State.PALADIN_BREACH);
                break;
            
            case 2:
                _fsm.ChangeState(FSM_State.PALADIN_SUMMON);
                break;
            
            case 3:
                _fsm.ChangeState(FSM_State.PALADIN_TACKLE);
                break;
            default:
                _fsm.ChangeState(FSM_State.PALADIN_CHASE);
                break;
        }
        
    }

    void ChangeTarget(object[] parameters)
    {
        Debug.Log("Change Chase");
        GameObject newTarget = (GameObject)parameters[0];
        target = newTarget.transform;
    }
}
