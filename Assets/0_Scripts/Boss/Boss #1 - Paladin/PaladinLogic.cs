using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PaladinLogic : MonoBehaviour
{
    public FiniteStateMachine fsm;
    public Animator animator;
    public List<ShieldObject> shieldRings;
    public FloorAttack floorAttack;
    public GameObject shieldPrefab;
    public Transform target;
    public Transform returnPoint;
    public float speed;

    public int attackNumber;
    public int maxAttackNumber;

    [Header("Rest")] public float timeToRest;
 
    private void Start()
    {
        StartRingPhase(1);
        fsm = new FiniteStateMachine();
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        fsm.AddState(PaladinState.REST, new PaladinRestState(fsm, animator, timeToRest, shieldRings, this));
        fsm.AddState(PaladinState.CHASE, new PaladinChaseState(target, this, fsm, animator, speed, 2));
        fsm.AddState(PaladinState.BREACH,
            new PaladinBreachState(floorAttack, animator, fsm, target, 3.3f, shieldRings, this));
        fsm.AddState(PaladinState.RETURN, new PaladinReturnState(returnPoint, fsm, this, 0.2f, speed));
        fsm.AddState(PaladinState.SUMMON, new PaladinSummonState(this, fsm, 7, 0.5f));
        fsm.ChangeState(PaladinState.REST);
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

    public void SummonDropShield()
    {
        int randH = UnityEngine.Random.Range(-1001, 1001);
        int randV = UnityEngine.Random.Range(-1001, 1001);

        float hValue = randH / 100f;
        float vValue = randV / 100f;

        GameObject shieldPrefab = Instantiate(this.shieldPrefab);
        shieldPrefab.transform.position = new Vector3(transform.position.x + hValue, transform.position.y,
            transform.position.z + vValue);
    }
}
