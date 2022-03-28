using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttacks : PlayerAttacks
{
    public GameObject weapon;
    public LayerMask pentadenteMask;
    public LayerMask filterMask;
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
        Collider[] colliders = Physics.OverlapSphere(weapon.transform.position, 2f);
        
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
            //Debug.Log(filteredColliders[0].gameObject.layer);

            if (filteredColliders[0].gameObject.layer == LayerMask.NameToLayer("Movable Object"))
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                StartCoroutine(GrabObject(8, filteredColliders[0].gameObject, pos));
            }

            else if (filteredColliders[0].gameObject.layer == LayerMask.NameToLayer("Breakable Object"))
            {
                filteredColliders[0].gameObject.SetActive(false);
            }

            else if (filteredColliders[0].gameObject.layer == LayerMask.NameToLayer("Grab Spot"))
            {
                //transform.position = weapon.transform.position;
                StartCoroutine(ClimbHook(1, weapon.transform.position));
            }
            else Debug.Log("Otro");
        }
        else
        {
            if(filteredColliders.Count == 0)
                Debug.Log("Menos de 1");
            
            if(filteredColliders.Count > 1)
                Debug.Log("Mas de 1");
        }
        
        EventManager.Trigger("ResetAbility");
        ThrowAbility(null);
    }

    public override void ThrowAbility(object[] parameters)
    {
        isConnected = false;
        ropeCollision.transform.position = transform.position;
        lineRenderer.enabled = false;
    }
    
    IEnumerator GrabObject(float duration, GameObject obj, Vector3 destiny)
    {
        weapon.GetComponent<BoxCollider>().enabled = false;
        float time = 0;
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            obj.transform.position = Vector3.Lerp(obj.transform.position, destiny, time / 2);
            if (duration % time >= 0.5f)
            {
                weapon.GetComponent<BoxCollider>().enabled = true;
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
            yield return new WaitForSeconds(0.01f);
        }
    }
}
