using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinTackleState : IState
{
    private FiniteStateMachine fsm;
    private PaladinBoss paladin;
    private Animator animator;
    private Transform target;
    private GameObject pathToTake;
    private float waitTime, waitCooldown1, waitCooldown2;
    
    private bool isReady;

    public PaladinTackleState(FiniteStateMachine fsm, PaladinBoss paladin, Animator animator, Transform target, GameObject pathToTake, float waitTime)
    {
        this.fsm = fsm;
        this.paladin = paladin;
        this.animator = animator;
        this.target = target;
        this.pathToTake = pathToTake;
        this.waitTime = waitTime;
    }

    public void OnStart()
    {
        target = paladin.target;
        EventManager.UnSubscribe("OnPlayerChange", ChangeTarget);
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        pathToTake.SetActive(true);
        isReady = false;
        waitCooldown1 = waitTime;
    }

    public void OnUpdate()
    {
        waitCooldown1 -= Time.deltaTime;
        waitCooldown2 -= Time.deltaTime;

        if (!isReady)
        {
            if (waitCooldown1 <= 0)
            {
                animator.Play("Paladin_Slide");
                pathToTake.SetActive(false);
                waitCooldown2 = waitTime;
                isReady = true;
            }
            else paladin.transform.LookAt(new Vector3(target.position.x, paladin.transform.position.y, target.position.z));
        }
        else
        {
            if (waitCooldown2 <= 0)
            {
                fsm.ChangeState(FSM_State.PALADIN_CAST);
            }
        }
    }

    public void OnExit()
    {
        
    }
    
    void ChangeTarget(object[] parameters)
    {
        Debug.Log("cambio");
        paladin.isDemon = !paladin.isDemon;
        
        if (paladin.isDemon)
        {
            target = paladin.angel;
        }
        else target = paladin.demon;
    }
}
