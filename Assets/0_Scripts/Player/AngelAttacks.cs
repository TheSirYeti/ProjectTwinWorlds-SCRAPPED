using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AngelAttacks : PlayerAttacks
{
    public GameObject weapon;
    public GameObject arrowPrefab;
    public LayerMask pentadenteMask;
    public LineRenderer lineRenderer;
    public bool isConnected;
    public GameObject ropeCollision;
    
    private void LateUpdate()
    {
        if (isConnected && weapon.activeSelf)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, weapon.transform.position);
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExecuteAbility();
            }
        }
        else
        {
            ThrowAbility(null);
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

    public override void AimAbility()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, pentadenteMask))
        {
            lineRenderer.enabled = true;
            lineRenderer.startColor = Color.gray;
            lineRenderer.startWidth = 0.3f;
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
            ropeCollision.transform.position = hit.point;
            isConnected = true;
        }
    }

    public void ExecuteAbility()
    {
        Collider[] colliders = Physics.OverlapSphere(weapon.transform.position, 1f);

        List<Collider> filteredColliders = new List<Collider>();
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer != LayerMask.NameToLayer("Default")
                && collider.gameObject.layer != LayerMask.NameToLayer("Wall")
                && collider.gameObject.layer != LayerMask.NameToLayer("Floor")
                && collider.gameObject.layer != LayerMask.NameToLayer("Pentadente"))
            {
                filteredColliders.Add(collider);
            }
        }

        if (filteredColliders.Count == 1)
        {
            GameObject objectCollided = filteredColliders[0].gameObject;

            switch (objectCollided.layer)
            {
                case (int)LayerStruct.LayerID.MOVABLE_OBJECT:
                    Vector3 movablePos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                    StartCoroutine(GrabObject(8, objectCollided, movablePos));
                    EventManager.Trigger("ResetAbility");
                    break;
                
                case (int)LayerStruct.LayerID.BREAKABLE_OBJECT:
                    filteredColliders[0].gameObject.SetActive(false);
                    EventManager.Trigger("ResetAbility");
                    break;
                
                case (int)LayerStruct.LayerID.GRAB_SPOT:
                    StartCoroutine(ClimbHook(1, weapon.transform.position));
                    break;
                
                case (int)LayerStruct.LayerID.SWING:
                    StartCoroutine(ClimbHook(1, objectCollided.GetComponent<SwingProperties>().GetFurthestLandingPosition(transform).position));
                    break;
                
                default:
                    Debug.Log("Otro");
                    EventManager.Trigger("ResetAbility");
                    break;
            }
        }
        
        else
        {
            if(filteredColliders.Count == 0)
                Debug.Log("Menos de 1");
            
            if(filteredColliders.Count > 1)
                Debug.Log("Mas de 1");
            
            EventManager.Trigger("ResetAbility");
        }
    }

    public override void ThrowAbility(object[] parameters)
    {
        isConnected = false;
        ropeCollision.transform.position = transform.position;
        lineRenderer.enabled = false;
    }
    
    IEnumerator GrabObject(float duration, GameObject obj, Vector3 destiny)
    {
        weapon.GetComponent<Collider>().enabled = false;
        float time = 0;
        
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            obj.transform.position = Vector3.Lerp(obj.transform.position, destiny, time / 2);
            
            if (duration % time >= 0.5f)
            {
                weapon.GetComponent<Collider>().enabled = true;
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    IEnumerator ClimbHook(float duration, Vector3 destiny)
    {
        float time = 0;
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(transform.position, destiny, time / 2);
            
            if(Vector3.Distance(transform.position, destiny) <= 0.3f)
                EventManager.Trigger("ResetAbility");
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
