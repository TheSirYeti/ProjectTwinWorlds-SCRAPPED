using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinReturnState : MonoBehaviour, IState
{
    private Transform returnPoint;
    private FiniteStateMachine fsm;
    private PaladinLogic paladin;
    private float minDistance, speed;

    public PaladinReturnState(Transform returnPoint, FiniteStateMachine fsm, PaladinLogic paladin, float minDistance, float speed)
    {
        this.returnPoint = returnPoint;
        this.fsm = fsm;
        this.paladin = paladin;
        this.minDistance = minDistance;
        this.speed = speed;
    }

    public void OnStart()
    {
        paladin.transform.LookAt(new Vector3(returnPoint.transform.position.x, paladin.transform.position.y, returnPoint.transform.position.z));
        paladin.animator.SetBool("isWalking", true);
        paladin.animator.Play("Paladin_Walking");
    }

    public void OnUpdate()
    {
        Vector3 direction = returnPoint.transform.position - paladin.transform.position;
        if (direction.magnitude >= minDistance)
        {
            direction.Normalize();
            paladin.transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            fsm.ChangeState(FSM_State.PALADIN_REST);
        }
    }
    
    public void OnExit()
    {
        paladin.animator.SetBool("isWalking", false);
    }
}
