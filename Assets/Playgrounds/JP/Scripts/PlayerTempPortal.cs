using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTempPortal : MonoBehaviour
{
    public Transform tpPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") || other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            PlayerWorlds.instance.angelPlayer.transform.position = tpPoint.position;
            PlayerWorlds.instance.demonPlayer.transform.position = tpPoint.position;
        }
    }
}
