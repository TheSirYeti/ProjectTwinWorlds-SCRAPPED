using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawBridgeLever : GenericLever
{
    public Animator myBridge;
    public ParticleSystem sparks;
    public int emitAmount;

    public override void OnLeverFlicked()
    {
        myBridge.Play("BridgeFall");
        SoundManager.instance.PlaySound(SoundID.SPARK);
        sparks.Emit(emitAmount);

        if (gameObject.GetComponent<FRadius>() != null)
        {
            EventManager.Trigger("OnHideF");
            Destroy(gameObject.GetComponent<FRadius>());
        }
    }
}
