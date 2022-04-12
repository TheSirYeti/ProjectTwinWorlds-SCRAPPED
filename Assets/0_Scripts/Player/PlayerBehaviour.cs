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
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventManager.Trigger("OnPlayerDeath");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            hp = 9999;
        }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            TakeDamage(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyAttack") || other.gameObject.layer == LayerMask.NameToLayer("Boss"))
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
