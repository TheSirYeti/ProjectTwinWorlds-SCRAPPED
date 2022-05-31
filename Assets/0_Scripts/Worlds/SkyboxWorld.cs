using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyboxWorld : MonoBehaviour
{
    private bool startDemon;
    public Material skyboxMatDemon, skyboxMatAngel;

    private void Start()
    {
        EventManager.Subscribe("OnPlayerChange", ChangeSkyboxMaterial);
        RenderSettings.skybox = skyboxMatDemon;
    }

    public void ChangeSkyboxMaterial(object[] parameters)
    {
        startDemon = !startDemon;
        if (startDemon)
        {
            RenderSettings.skybox = skyboxMatDemon;
        }
        else
        {
            RenderSettings.skybox = skyboxMatAngel;
        }
    }
}
