using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class PaladinTackleState : MonoBehaviour, IState
{
    private float timeToCharge;
    private float timeToNextState;
    private float currentTime;

    private bool isTackling = false;

    private FiniteStateMachine fsm;
    private Animator animator;
    private PaladinLogic paladin;
    private List<Transform> spawnPoints;
    private GameObject shieldPrefab;
    private Vector3 target;
    
    public PaladinTackleState(float timeToCharge, float timeToNextState, FiniteStateMachine fsm, Animator animator, PaladinLogic paladin, List<Transform> spawnPoints, GameObject shieldPrefab)
    {
        this.timeToCharge = timeToCharge;
        this.timeToNextState = timeToNextState;
        this.fsm = fsm;
        this.animator = animator;
        this.paladin = paladin;
        this.spawnPoints = spawnPoints;
        this.shieldPrefab = shieldPrefab;
    }

    public void OnStart()
    {
        currentTime = 0;
        animator.Play("Paladin_Idle");
        paladin.SetShieldSpeeds(ShieldObject.SpeedState.FAST);
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= timeToCharge && !isTackling)
        {
            animator.SetTrigger("onTackle");
            isTackling = true;
            target = paladin.target.position;
        }

        if (currentTime >= timeToNextState)
        {
            SetUpShields();
            fsm.ChangeState(PaladinState.CHASE);
        }
        else if(isTackling)
        {
            Vector3 direction = target - paladin.transform.position;
            paladin.transform.LookAt(new Vector3(direction.x, paladin.transform.position.y, direction.z));
            direction.Normalize();
            paladin.transform.position += direction * (paladin.speed * 4) * Time.deltaTime;
        }
    }

    public void OnExit()
    {
        paladin.attackNumber++;
        paladin.SetShieldSpeeds(ShieldObject.SpeedState.NORMAL);
    }

    public void SetUpShields()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject shield = Instantiate(shieldPrefab);
            shield.transform.position = spawnPoint.transform.position;
            shield.transform.rotation = spawnPoint.transform.rotation;
        }
    }
}
