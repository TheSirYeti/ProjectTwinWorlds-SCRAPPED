using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnasRompibles : InteractableObject
{
    bool isBroken = false;
    bool isFalling = false;

    [SerializeField] private Collider _firstCollider;
    [SerializeField] private Collider _secondCollider;

    [SerializeField] private GameObject firstCollumn;
    [SerializeField] private GameObject secondCollumn;

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
        EventManager.Trigger("ResetAbility");
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
            firstCollumn.gameObject.SetActive(false);
            secondCollumn.gameObject.SetActive(true);
            TurnOffCollider();
            TurnOnCollider();
            StartCoroutine(WaitRespawn());
        }
        else
            OnObjectEnd();
    }

    public void TurnOffCollider()
    {
        _firstCollider.enabled = false;
    }

    public void TurnOnCollider()
    {
        _secondCollider.enabled = true;
    }

    public void ChangeBool(bool b)
    {
        isFalling = b;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MiniBoss" && isFalling)
        {
            Debug.Log("ola?");
            collision.gameObject.GetComponent<MiniBossController>().TakeDamage();
            isFalling = false;
        }

        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.transform.position += new Vector3(2, 0, 0);
        }

    }

    IEnumerator WaitRespawn()
    {
        yield return new WaitForSeconds(3f);
        _firstCollider.enabled = true;
        _secondCollider.enabled = false;
        firstCollumn.gameObject.SetActive(true);
        secondCollumn.gameObject.SetActive(false);
        ChangeBool(false);
        isBroken = false;
    }
}
