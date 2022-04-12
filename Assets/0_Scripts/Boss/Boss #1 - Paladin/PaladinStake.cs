using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaladinStake : InteractableObject
{
    public Animator animator;
    public GameObject stake;
    public Collider myCollider;
    public int stakeId;

    private void Awake()
    {
        EventManager.Subscribe("OnBossDamagableTriggered", MoveStake);
        EventManager.Subscribe("OnBossDamaged", StopStake);
        EventManager.Subscribe("OnBossRestingOver", ResetStake);
        EventManager.Subscribe("OnBossResting", SetStake);
    }

    public override void OnObjectStart()
    {
        EventManager.UnSubscribe("OnBossDamagableTriggered", MoveStake);
        EventManager.UnSubscribe("OnBossDamaged", StopStake);
        EventManager.UnSubscribe("OnBossRestingOver", ResetStake);
        EventManager.UnSubscribe("OnBossResting", SetStake);
        EventManager.Subscribe("OnBossDamagableTriggered", MoveStake);
        EventManager.Subscribe("OnBossDamaged", StopStake);
        EventManager.Subscribe("OnBossRestingOver", ResetStake);
        EventManager.Subscribe("OnBossResting", SetStake);

        object[] ids = new object[1];
        ids[0] = stakeId;
        MoveStake(ids);
    }

    public override void OnObjectDuring()
    {
        //throw new NotImplementedException();
    }

    public override void OnObjectEnd()
    {
        //
    }

    private void SetStake(object[] parameters)
    {
        myCollider.enabled = true;
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
        myCollider.enabled = false;
        stake.SetActive(false);
    }

    private void StopStake(object[] parameters)
    {
        animator.Play("Estaca_Idle");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)LayerStruct.LayerID.BOSS)
        {
            EventManager.Trigger("OnBossDamaged");
            EventManager.Trigger("ResetAbility");
            ResetStake(null);
            ResetVariables(null);
        }
        
        if (collision.gameObject.layer == (int)LayerStruct.LayerID.BREAKABLE_OBJECT)
        {
            animator.Play("Estaca_Idle");
            ResetStake(null);
            ResetVariables(null);
        }
    }
}
