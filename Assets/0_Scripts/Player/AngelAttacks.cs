using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AngelAttacks : PlayerAttacks
{
    public GameObject weapon;
    public Transform arrowEndPoint;
    public GameObject arrowPrefab;
    public LayerMask pentadenteMask;
    public LineRenderer lineRenderer;
    public bool isConnected;
    public bool isSwinging;
    public bool isPulling;
    public bool canHang;
    public GameObject ropeCollision;
    
    private void LateUpdate()
    {
        if (isConnected)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, arrowEndPoint.position);
        }
    }

    public override void GenerateBasicAttack()
    {
        cooldownTimer = attackCooldown;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = transform.position;
            arrow.transform.forward = transform.forward;
        }

        _playerObserver.NotifySubscribers("BasicAttack");
    }

    public override void AimAbility(Transform position, InteractableObject intObj, bool isFirst)
    {
        transform.LookAt(new Vector3(position.position.x, transform.position.y, position.position.z));
        weapon.transform.position = transform.position;
        weapon.gameObject.SetActive(true);
        weapon.transform.forward = transform.position - position.position;

        StartCoroutine(AimArrow(position, 0.02f, intObj, isFirst));
    }

    public override void ExecuteAbility()
    {
        if (currentObject != null && currentObject.isObjectTriggered)
        {
            currentObject.OnObjectExecute();
        }
    }

    public override void ThrowAbility(object[] parameters)
    {
        ropeCollision.transform.position = transform.position;
        lineRenderer.enabled = false;
        isSwinging = false;
        usedAbility = false;
        isConnected = false;
        weapon.transform.parent = null;
        weapon.gameObject.SetActive(false);
        
        if (currentObject != null)
        {
            Debug.Log("Loop check 1 - angel");
            SoundManager.instance.PlaySound(SoundID.BOW_PULL);
            
            if(currentObject.isObjectTriggered)
                currentObject.OnObjectEnd();
        }
        currentObject = null;
        
        Debug.Log("Loop check 2 - angel");
        EventManager.Trigger("OnPulleyStop");
        EventManager.Trigger("OnSwingStop");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            canHang = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Movable Object"))
        {
            canHang = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canHang = false;
    }

    IEnumerator AimArrow(Transform position, float minDistance, InteractableObject intObj, bool isFirst)
    {
        PlayerWorlds.instance.isShooting = true;
        float time = 0f;
        float duration = 0.5f;
        
        while(Vector3.Distance(weapon.transform.position, position.position) >= minDistance)
        {
            yield return new WaitForSeconds(0.0001f);
            time += Time.deltaTime;
            weapon.transform.position = Vector3.Lerp(transform.position, position.position, time / duration);
        }
        
        lineRenderer.SetPosition(0, transform.position);
        arrowEndPoint = position;
        lineRenderer.SetPosition(1, arrowEndPoint.position);
        isConnected = true;
        lineRenderer.enabled = true;
        SoundManager.instance.PlaySound(SoundID.ARROW_ROPE);

        if (!isFirst)
        {
            currentObject = intObj;
            currentObject.OnObjectStart();
        }
        else
            intObj.isFirstTriggered = true;
        
        weapon.transform.SetParent(position);
        PlayerWorlds.instance.isShooting = false;
        yield return new WaitForSeconds(0.001f);
    }
}
