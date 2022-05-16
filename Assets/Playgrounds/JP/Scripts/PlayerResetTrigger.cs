using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResetTrigger : MonoBehaviour
{
    public bool forDemon, forAngel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("AngelPlayer") && forAngel)
        {
            EventManager.Trigger("ResetAbility", PlayerWorlds.instance.angelPlayer);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonPlayer") && forDemon)
        {
            EventManager.Trigger("ResetAbility", PlayerWorlds.instance.angelPlayer);
        }
    }
}
