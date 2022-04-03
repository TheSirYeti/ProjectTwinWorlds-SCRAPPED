using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinLogic : MonoBehaviour
{
    public FiniteStateMachine fsm;
    public Animator animator;
    public List<ShieldObject> shieldRings;
    public FloorAttack floorAttack;
    public GameObject shieldPrefab;
    public Transform target;

    public int attackNumber;
    public int maxAttackNumber;
    
    [Header("Rest")] 
    public float timeToRest;
    
    private void Start()
    {
        StartRingPhase(1);
        fsm = new FiniteStateMachine();
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        fsm.AddState(PaladinState.REST, new PaladinRestState(fsm, animator, timeToRest, shieldRings));
        fsm.AddState(PaladinState.CHASE, new PaladinChaseState(target, this, fsm,animator,2,1));
        fsm.AddState(PaladinState.BREACH, new PaladinBreachState(floorAttack, animator, fsm, target, 3.3f, shieldRings, this));
        fsm.ChangeState(PaladinState.CHASE);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public void StartRingPhase(int phaseID)
    {
        switch (phaseID)
        {
            case 1:
                EnableRings(shieldRings[0]);
                break;
            case 2:
                EnableRings(shieldRings[0]);
                EnableRings(shieldRings[1]);
                break;
            case 3:
                EnableRings(shieldRings[0]);
                EnableRings(shieldRings[1]);
                EnableRings(shieldRings[2]);
                break;
        }
    }

    public void EnableRings(ShieldObject shield)
    {
        shield.gameObject.SetActive(true);
    }
}
