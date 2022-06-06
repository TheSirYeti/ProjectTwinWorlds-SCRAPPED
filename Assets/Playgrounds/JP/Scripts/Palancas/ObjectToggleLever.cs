using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggleLever : GenericLever
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private int emitAmount;
    
    public override void OnLeverFlicked()
    {
        foreach (var obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
        
        SoundManager.instance.PlaySound(SoundID.SPARK);
        sparks.Emit(emitAmount);

        if (gameObject.GetComponent<FRadius>() != null)
        {
            EventManager.Trigger("OnHideF");
            Destroy(gameObject.GetComponent<FRadius>());
        }
    }
}
