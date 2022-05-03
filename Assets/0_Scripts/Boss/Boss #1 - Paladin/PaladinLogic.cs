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
    public List<GameObject> stakes;
    public float speed;

    public int attackNumber;
    public int maxAttackNumber;

    public int currentPhase;

    [Header("Rest")] public float timeToRest;
 
    private void Start()
    {
        SoundManager.instance.PlayMusic(MusicID.BOSS);
        EventManager.Subscribe("OnBossDamaged", SetPhaseState);
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        EventManager.Subscribe("OnBossResting", OnRestStart);
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
            fsm.ChangeState(FSM_State.PALADIN_DEATH);
            animator.Play("Paladin_Death");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            StartRingPhase(currentPhase);
            maxAttackNumber += 2;
            fsm.ChangeState(FSM_State.PALADIN_CHASE);
        }
    }

    void SetFSM()
    {
        fsm = new FiniteStateMachine();
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        fsm.AddState(FSM_State.PALADIN_REST, new PaladinRestState(fsm, animator, timeToRest, shieldRings, this));
        fsm.AddState(FSM_State.PALADIN_CHASE, new PaladinChaseState(target, this, fsm, animator, speed, 30));
        fsm.AddState(FSM_State.PALADIN_BREACH,
            new PaladinBreachState(floorAttack, animator, fsm, target, 3.8f, shieldRings, this));
        fsm.AddState(FSM_State.PALADIN_RETURN, new PaladinReturnState(returnPoint, fsm, this, 0.2f, speed));
        fsm.AddState(FSM_State.PALADIN_SUMMON, new PaladinSummonState(this, fsm, 7, 0.8f));
        fsm.AddState(FSM_State.PALADIN_TACKLE, new PaladinTackleState(1f, 4f, fsm, animator, this, shieldSpawnpoints, shieldPrefabOut, GetComponent<Collider>()));
        fsm.AddState(FSM_State.PALADIN_DEATH, new NullState());
        fsm.ChangeState(FSM_State.PALADIN_CHASE);
    }

    public void SetShieldSpeeds(ShieldObject.SpeedState state)
    {
        foreach (var shield in shieldRings)
        {
            shield.SetSpeed(state);
        }
    }

    public void ChangeTarget(object[] parameters)
    {
        Debug.Log("Change paladin");
        GameObject newTarget = (GameObject)parameters[0];
        target = newTarget.transform;
    }
    
    public void OnRestStart(object[] parameters)
    {
        foreach (GameObject stake in stakes)
        {
            stake.SetActive(true);
        }
    }

    public void OnPaladinDeath()
    {
        LevelManager.instance.LoadNextScene(0);
    }
}
