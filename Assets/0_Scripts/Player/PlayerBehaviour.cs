using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int hp;
    public bool isDemon;
    
    private void Start()
    {
        EventManager.Subscribe("OnDamageRecieved", TakeDamage);
    }

    public void TakeDamage(object[] parameters)
    {
        hp--;
        EventManager.Trigger("OnPlayerHPUpdated", isDemon);
        SoundManager.instance.PlaySound(SoundID.PLAYER_DAMAGE);
        
        
        if (hp <= 0)
        {
            EventManager.Trigger("OnPlayerDeath");
            Debug.Log("Se murio");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
        {
            TakeDamage(null);
        }
    }
}
