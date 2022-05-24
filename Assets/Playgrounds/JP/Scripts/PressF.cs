using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressF : MonoBehaviour
{
    public GameObject FinScreen;

    private void Start()
    {
        EventManager.Subscribe("OnHideF", HideF);
        EventManager.Subscribe("OnShowF", ShowF);
    }

    public void ShowF(object[] parameters)
    {
        FinScreen.SetActive(true);
    }

    public void HideF(object[] parameters)
    {
        FinScreen.SetActive(false);
    }
}
