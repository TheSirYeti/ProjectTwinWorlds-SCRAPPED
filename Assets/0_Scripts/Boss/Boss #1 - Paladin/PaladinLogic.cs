using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PaladinLogic : MonoBehaviour
{
    public FiniteStateMachine fsm;
    public Animator animator;
    public List<ShieldObject> shieldRings;
    public FloorAttack floorAttack;
    public GameObject shieldPrefabDown, shieldPrefabOut;
    public List<Transform> shieldSpawnpoints;
    public Transform target;
    public Transform returnPoint;
    public float speed;

    public int attackNumber;
    public int maxAttackNumber;

    public int currentPhase;

    [Header("Rest")] public float timeToRest;
 
    private void Start()
    {
        EventManager.Subscribe("OnBossDamaged", SetPhaseState);
        currentPhase = 1;
        StartRingPhase(currentPhase);
        SetFSM();
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public void StartRingPhase(int phaseID)
    {
        Debug.Log(currentPhase);
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
        shield.ResetShields();
    }

    public void SummonDropShield()
    {
        int randH = UnityEngine.Random.Range(-301, 301);
        int randV = UnityEngine.Random.Range(-301, 301);

        float hValue = randH / 100f;
        float vValue = randV / 100f;

        GameObject shieldPrefab = Instantiate(this.shieldPrefabDown);
        shieldPrefab.transform.position = new Vector3(target.transform.position.x + hValue, target.transform.position.y,
            target.transform.position.z + vValue);
    }

    public void SetPhaseState(object[] parameters)
    {
        currentPhase++;
        
        if (currentPhase > 3)
        {
            Destroy(gameObject);
        }
        else
        {
            StartRingPhase(currentPhase);
            maxAttackNumber += 2;
            fsm.ChangeState(PaladinState.CHASE);
        }
    }

    void SetFSM()
    {
        fsm = new FiniteStateMachine();
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        fsm.AddState(PaladinState.REST, new PaladinRestState(fsm, animator, timeToRest, shieldRings, this));
        fsm.AddState(PaladinState.CHASE, new PaladinChaseState(target, this, fsm, animator, speed, 13));
        fsm.AddState(PaladinState.BREACH,
            new PaladinBreachState(floorAttack, animator, fsm, target, 3.3f, shieldRings, this));
        fsm.AddState(PaladinState.RETURN, new PaladinReturnState(returnPoint, fsm, this, 0.2f, speed));
        fsm.AddState(PaladinState.SUMMON, new PaladinSummonState(this, fsm, 7, 0.35f));
        fsm.AddState(PaladinState.TACKLE, new PaladinTackleState(1f, 4f, fsm, animator, this, shieldSpawnpoints, shieldPrefabOut));
        fsm.ChangeState(PaladinState.CHASE);
    }

    public void SetShieldSpeeds(ShieldObject.SpeedState state)
    {
        foreach (var shield in shieldRings)
        {
            shield.SetSpeed(state);
        }
    }
}
