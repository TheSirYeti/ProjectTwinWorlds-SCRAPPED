using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MafiaShootingState : IState
{
    private FiniteStateMachine fsm;
    private MafiaLogic mafia;
    private float currentShot;

    public MafiaShootingState(FiniteStateMachine fsm, MafiaLogic mafia)
    {
        this.fsm = fsm;
        this.mafia = mafia;
    }

    public void OnStart()
    {
        mafia.currentShot = 0;
        mafia.canShoot = true;
        mafia.StartCoroutine(mafia.ShotCycle());
    }

    public void OnUpdate()
    {
        mafia.gunPointAngel.LookAt(new Vector3(mafia.angel.position.x, mafia.gunPointAngel.position.y, mafia.angel.position.z));
        mafia.gunPointDemon.LookAt(new Vector3(mafia.demon.position.x, mafia.gunPointDemon.position.y, mafia.demon.position.z));
        
        if (mafia.currentShot > mafia.maxShotCount)
        {
            mafia.canShoot = false;
            mafia.StopCoroutine(mafia.ShotCycle());
            fsm.ChangeState(FSM_State.MAFIA_RELOAD);
        }
    }

    public void OnExit()
    {
        //throw new System.NotImplementedException();
    }

}
