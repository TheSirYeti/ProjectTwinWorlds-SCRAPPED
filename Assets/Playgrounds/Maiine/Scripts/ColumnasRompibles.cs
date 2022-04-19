using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnasRompibles : InteractableObject
{
    bool isBroken = false;
    bool isFalling = false;

    [SerializeField] private BoxCollider _firstCollider;
    [SerializeField] private BoxCollider _secondCollider;

    public void InitialBreak()
    {
        //particulas y sonido
        isBroken = true;
    }

    public override void OnObjectDuring()
    {
        
    }

    public override void OnObjectEnd()
    {
        ResetVariables(null);
    }

    public override void OnObjectExecute()
    {
        
    }

    public override void OnObjectStart()
    {
        PentadentBreak();
        isObjectTriggered = true;
    }

    public void PentadentBreak()
    {
        if (isBroken)
        {
            ChangeBool(true);
            //mas sonido y particulas
            //animacion
        }
        else
            OnObjectEnd();
    }

    public void TurnOffCollider()
    {
        _firstCollider.gameObject.SetActive(false);
    }

    public void TurnOnCollider()
    {
        _secondCollider.gameObject.SetActive(true);
    }

    public void ChangeBool(bool b)
    {
        isFalling = b;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MiniBoss" && isFalling)
        {
            collision.gameObject.GetComponent<MiniBossController>().TakeDamage();
        }
    }
}
