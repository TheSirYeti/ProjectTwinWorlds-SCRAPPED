using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyboxWorld : MonoBehaviour
{
    public Material skyboxMatDemon, skyboxMatAngel;

    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ChangeSkyboxMaterial);
        RenderSettings.skybox = skyboxMatDemon;
    }

    public void ChangeSkyboxMaterial(object[] parameters)
    {
        GameObject player = parameters[0] as GameObject;

        if (player.layer == LayerMask.NameToLayer("DemonPlayer"))
        {
            RenderSettings.skybox = skyboxMatDemon;
        }
        else
        {
            RenderSettings.skybox = skyboxMatAngel;
        }
    }
}
