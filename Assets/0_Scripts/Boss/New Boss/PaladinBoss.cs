using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinBoss : MonoBehaviour
{
    [Header("Stats")] 
    [SerializeField] private float hp = 3;
    private float shieldCount;

    [Header("Collections")] 
    [SerializeField] private List<GameObject> shields;

    [Header("Transforms")] 
    [SerializeField] private Transform target;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform topPosition;

    [Header("References")] 
    [SerializeField] private Transform demon, angel;
    private bool isDemon = true;
    
    [Header("VFX")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem poof1, poof2;

    [Header("Values for Summon")] 
    [SerializeField] private SwordSummoning swordSummoning;
    [SerializeField] private float waitTimeForSummon;
    [SerializeField] private int swordAmountForSummon;
    [SerializeField] private int loopAmountForSummon;
    
    [Header("Values for Casting")] 
    [SerializeField] private GameObject bulletPrefab;

    [Header("Values for Casting")] 
    [SerializeField] private GameObject pathForTackle;
    
    
    private FiniteStateMachine fsm;

    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ChangeTarget);
        target = demon;
        fsm = new FiniteStateMachine();
        fsm.AddState(FSM_State.PALADIN_CAST, new PaladinShootingState(fsm, this, animator, 10, 
            1f, 3f, transform, target, centerPosition, topPosition, bulletPrefab, poof1, poof2));
        
        fsm.AddState(FSM_State.PALADIN_NEXTMOVE, new NullState());
        fsm.AddState(FSM_State.PALADIN_SUMMON, new PaladinSummonState(fsm, this, animator, swordSummoning, swordAmountForSummon, 
            loopAmountForSummon, waitTimeForSummon));
        fsm.AddState(FSM_State.PALADIN_TACKLE, new PaladinTackleState(fsm, this, animator, target, pathForTackle, 2f));
        fsm.AddState(FSM_State.PALADIN_REST, new PaladinRestState(fsm, this, animator, 10f));
        fsm.ChangeState(FSM_State.PALADIN_TACKLE);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public GameObject InstantiateBullet(GameObject bullet)
    {
        return Instantiate(bullet);
    }

    void ChangeTarget(object[] parameters)
    {
        isDemon = !isDemon
        if (target == demon)
        {
            target = angel;
        }
        else target = demon;
    }
}
