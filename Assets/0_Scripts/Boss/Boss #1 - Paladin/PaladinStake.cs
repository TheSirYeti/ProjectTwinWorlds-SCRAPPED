using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinStake : MonoBehaviour
{
    public Animator animator;
    public GameObject stake;
    public int stakeId;
    private void Start(){
    
        EventManager.Subscribe("OnBossDamagableTriggered", MoveStake);
        EventManager.Subscribe("OnBossDamaged", StopStake);
        EventManager.Subscribe("OnBossRestingOver", ResetStake);
        EventManager.Subscribe("OnBossResting", SetStake);
    }

    private void SetStake(object[] parameters)
    {
        stake.SetActive(true);
    }
    
    private void MoveStake(object[] parameters)
    {
        if((int)parameters[0] == stakeId)
            animator.SetTrigger("doDrop");
    }

    private void ResetStake(object[] parameters)
    {
        animator.Play("Estaca_Idle");
        stake.SetActive(false);
    }

    private void StopStake(object[] parameters)
    {
        animator.Play("Estaca_Idle");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HOLA COLLIDERSSSS");
        
        if (collision.gameObject.layer == (int)LayerStruct.LayerID.BOSS)
        {
            EventManager.Trigger("OnBossDamaged");
        }
        
        if (collision.gameObject.layer == (int)LayerStruct.LayerID.BREAKABLE_OBJECT)
        {
            animator.Play("Estaca_Idle");
            stake.SetActive(false);
        }
    }
}
