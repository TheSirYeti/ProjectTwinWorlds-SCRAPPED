using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject ropeCollision;
    
    private void LateUpdate()
    {
        if (isConnected)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, arrowEndPoint.position);
            
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

    public override void AimAbility(Transform position)
    {
        lineRenderer.SetPosition(0, transform.position);
        arrowEndPoint = position;
        lineRenderer.SetPosition(1, arrowEndPoint.position);
        isConnected = true;
        lineRenderer.enabled = true;
    }

    public override void ExecuteAbility()
    {
        var collidedObject = CheckColliders();

        if (collidedObject != null && collidedObject.CheckForFirstTrigger() && LayerMask.NameToLayer(collidedObject.layerTrigger) == gameObject.layer)
        {
            collidedObject.OnObjectEnd();
        }
    }
    
    InteractableObject CheckColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(weapon.transform.position, 1f, collidableLayer);

        if (colliders.Length == 1)
        {
            return colliders[0].GetComponent<InteractableObject>();
        }
        else return null;
    }

    public override void ThrowAbility(object[] parameters)
    {
        isConnected = false;
        ropeCollision.transform.position = transform.position;
        lineRenderer.enabled = false;
        isSwinging = false;
        EventManager.Trigger("OnSwingStop");
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

    IEnumerator SwingMovement(SwingProperties swingProperties)
    {
        float time = 0;
        int currentWaypoint = swingProperties.GetNearestLandingPosition(transform);

        int nextWaypoint = swingProperties.DetermineOrientation(currentWaypoint);

        while (isSwinging)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, swingProperties.swingPositions[currentWaypoint].position, time / 0.2f);
            
            
            if (Vector3.Distance(transform.position, swingProperties.swingPositions[currentWaypoint].position) <= 0.2f)
            if (Vector3.Distance(transform.position, swingProperties.swingPositions[currentWaypoint].position) <= 0.2f)
            {
                currentWaypoint += nextWaypoint;

                if (currentWaypoint >= swingProperties.swingPositions.Count - 1 || currentWaypoint < 1)
                {
                    nextWaypoint *= -1;
                }

                time = 0f;
            }
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
