using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceptor : MonoBehaviour, IDoorTriggerable
{
    [SerializeField] private bool isRecieving;

    [SerializeField] private List<Renderer> itemsToChange;
    [SerializeField] private List<Material> goodMaterials, badMaterials;
    [SerializeField] private CheckExitStatus exitStatus;

    public void SetRecieverStatus(bool value)
    {
        if (value != isRecieving)
        {
            isRecieving = value;

            if (isRecieving)
            {
                SoundManager.instance.PlaySound(SoundID.SUCCESS);
                SetObjectMaterials(goodMaterials);
            }
            else
            {
                SetObjectMaterials(badMaterials);
            }
            exitStatus.CheckStatus();
        }
    }

    public void SetObjectMaterials(List<Material> materials)
    {
        for (int i = 0; i < itemsToChange.Count; i++)
        {
            itemsToChange[i].material = materials[i];
        }
    }


    public bool IsTriggerableActive()
    {
        return isRecieving;
    }
}
