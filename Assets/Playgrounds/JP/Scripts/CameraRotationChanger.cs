using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraRotationChanger : MonoBehaviour
{
    public Transform transformValues;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DemonOnlyTrigger") ||
            other.gameObject.layer == LayerMask.NameToLayer("AngelOnlyTrigger"))
        {
            PlayerWorlds.instance.vCamera.transform.position = transformValues.position;
            PlayerWorlds.instance.vCamera.transform.rotation = transformValues.rotation;
        }
    }
}
