using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int hp;
    public bool isDemon;
    public bool buffer;
    
    private void Start()
    {
        EventManager.Subscribe("OnDamageRecieved", TakeDamage);
    }

    public void TakeDamage(object[] parameters)
    {
        if (!buffer)
        {
            hp--;
            EventManager.Trigger("OnPlayerHPUpdated", isDemon);
            SoundManager.instance.PlaySound(SoundID.PLAYER_DAMAGE);
            StartCoroutine(GiveBuffer());
        }


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

    IEnumerator GiveBuffer()
    {
        buffer = true;
        yield return new WaitForSeconds(1f);
        buffer = false;
    }
}
