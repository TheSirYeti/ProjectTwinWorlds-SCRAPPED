using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaReloadingState : IState
{
    private FiniteStateMachine fsm;
    private MafiaLogic mafia;
    private float timeToReload, currentTime;

    public MafiaReloadingState(FiniteStateMachine fsm, MafiaLogic mafia, float timeToReload)
    {
        this.fsm = fsm;
        this.mafia = mafia;
        this.timeToReload = timeToReload;
    }

    public void OnStart()
    {
        currentTime = 0f;
        mafia.reloadSign.SetActive(true);
    }

    public void OnUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime > timeToReload)
        {
            fsm.ChangeState(FSM_State.MAFIA_SHOOT);
        }
    }

    public void OnExit()
    {
        mafia.reloadSign.SetActive(false);
    }
}
