using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MafiaShootingState : IState
{
    private FiniteStateMachine fsm;
    private Transform angel, demon;
    private GameObject bulletPrefab;
    
    public void OnStart()
    {
        EventManager.Subscribe("OnPlayerChange", ChangePlayers);
        
        if (PlayerWorlds.instance.angelPlayer.activeSelf)
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

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
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
}
