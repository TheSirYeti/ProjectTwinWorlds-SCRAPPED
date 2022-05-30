using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TotemButton : GenericLever
{
    public TotemPillar pillar;

    public List<GameObject> objectsToChangeMat;
    public GameObject portal;
    public List<Material> materials;

    public override void OnLeverFlicked()
    {
        for (int i = 0; i < objectsToChangeMat.Count; i++)
        {
            objectsToChangeMat[i].GetComponent<Renderer>().material = materials[i];
        }
        pillar.EnableTotem();
        portal.SetActive(false);
        SoundManager.instance.PlaySound(SoundID.SUCCESS);
    }
}
