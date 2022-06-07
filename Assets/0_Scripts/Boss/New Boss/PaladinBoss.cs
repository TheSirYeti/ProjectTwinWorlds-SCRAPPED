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
    public Transform target;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform topPosition;

    [Header("References")] public List<GameObject> wave1Objects, wave2Objects, wave3Objects;
    public Transform demon, angel;
    public bool isDemon = true;
    
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
        DoHPStatusCheck();
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

    public void SetFSM()
    {
        
    }
    
    public GameObject InstantiateBullet(GameObject bullet)
    {
        return Instantiate(bullet);
    }

    void ChangeTarget(object[] parameters)
    {
        Debug.Log("cambio");
        isDemon = !isDemon;
        
        if (isDemon)
        {
            target = angel;
        }
        else target = demon;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BossDamagable"))
        {
            Destroy(other.gameObject);
            hp--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BossDamagable"))
        {
            Destroy(collision.gameObject);
            hp--;
            DoHPStatusCheck();
        }
    }

    public void DoHPStatusCheck()
    {
        foreach (var obj in wave1Objects)
        {
            obj.SetActive(false);
        } 
        foreach (var obj in wave2Objects)
        {
            obj.SetActive(false);
        }
        foreach (var obj in wave3Objects)
        {
            obj.SetActive(false);
        }
        
        if (hp > 0)
        {
            switch (hp)
            {
                case 3:
                    foreach (var obj in wave1Objects)
                    {
                        obj.SetActive(true);
                    }
                    break;
                case 2:
                    foreach (var obj in wave2Objects)
                    {
                        obj.SetActive(true);
                    }
                    break;
                case 1:
                    foreach (var obj in wave3Objects)
                    {
                        obj.SetActive(true);
                    }
                    break;
            }
        }
    }
}
