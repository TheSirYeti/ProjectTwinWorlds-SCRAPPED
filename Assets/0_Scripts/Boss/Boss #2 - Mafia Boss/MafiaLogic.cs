using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MafiaLogic : MonoBehaviour
{
    public int hp;
    private FiniteStateMachine fsm;
    public GameObject bulletPrefab;
    public Transform angel, demon;
    public Transform gunPointAngel, gunPointDemon, spawnPointAngel, spawnPointDemon;
    public float reloadTime, shotTime;
    public int currentShot, maxShotCount;
    public bool canShoot;
    public GameObject reloadSign;
    public Transform spawnPoint;
    
    
    public List<GameObject> wallWaves;
    public List<int> shotAmount;

    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ChangePlayers);

        fsm = new FiniteStateMachine();
        fsm.AddState(FSM_State.MAFIA_SHOOT, new MafiaShootingState(fsm, this));
        fsm.AddState(FSM_State.MAFIA_RELOAD, new MafiaReloadingState(fsm, this, reloadTime));
        
        fsm.ChangeState(FSM_State.MAFIA_RELOAD);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public IEnumerator ShotCycle()
    {
        while (canShoot)
        {
            GameObject bullet1 = Instantiate(bulletPrefab);
            GameObject bullet2 = Instantiate(bulletPrefab);

            bullet1.transform.position = spawnPointAngel.position;
            Vector3 aimPositionAngel = new Vector3(angel.position.x, angel.position.y - 0.3f, angel.position.z);
            bullet1.transform.forward = aimPositionAngel - gunPointAngel.position;
        
            bullet2.transform.position = spawnPointDemon.position;
            Vector3 aimPositionDemon = new Vector3(demon.position.x, demon.position.y - 0.3f, demon.position.z);
            bullet2.transform.forward = aimPositionDemon - gunPointDemon.position;

            currentShot++;
            Debug.Log(currentShot);
        
            yield return new WaitForSeconds(shotTime);
        }
    }
    
    public void ChangePlayers(object[] parameters)
    {
        GameObject player = (GameObject)parameters[0];
        
        if (player.layer == LayerMask.NameToLayer("AngelPlayer"))
        {
            angel = PlayerWorlds.instance.angelPlayer.transform;
            demon = PlayerWorlds.instance.demonTotem.transform;
        }
        else
        {
            angel = PlayerWorlds.instance.angelTotem.transform;
            demon = PlayerWorlds.instance.demonPlayer.transform;
        }
    }

    public void TakeDamage()
    {
        hp--;
        
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            wallWaves[hp - 1].SetActive(true);
            wallWaves[hp].SetActive(false);


            PlayerWorlds.instance.angelPlayer.transform.position = spawnPoint.position;
            PlayerWorlds.instance.demonPlayer.transform.position = spawnPoint.position;
            
            maxShotCount = shotAmount[hp];
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            if (collision.gameObject.GetComponent<MovableItem>().isFalling)
            {
                collision.gameObject.GetComponent<MovableItem>().OnObjectEnd();
                TakeDamage();
                collision.gameObject.SetActive(false);
            }
        }
    }
}
