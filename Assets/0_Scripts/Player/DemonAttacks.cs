using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonAttacks : PlayerAttacks
{
    public PentadentCollision weapon;
    public float flightDuration;
    public override void GenerateBasicAttack()
    {
        cooldownTimer = attackCooldown;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
        
        _playerObserver.NotifySubscribers("BasicAttack");
    }

    public override void AimAbility(Vector3 position)
    {
        weapon.gameObject.SetActive(true);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            weapon.transform.position = transform.position;
            weapon.transform.forward = transform.position - hit.point;
            weapon.currentDestination = hit.point;
            StartCoroutine(weapon.ThrowPentadent());
        }
    }

    public override void ThrowAbility(object[] parameters)
    {
        Debug.Log(weapon.gameObject);
        weapon.StopCoroutine(weapon.ThrowPentadent());
        weapon.transform.SetParent(null);
        weapon.transform.position = transform.position;
        weapon.foundParent = false;
        weapon.gameObject.SetActive(false);
    }

    public void AimPentadente()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, collidableLayer))
        {
            CheckForCollisions(hit.point);
        }
        
    }

    public void CheckForCollisions(Vector3 objectPosition)
    {
        Collider[] collisions = Physics.OverlapSphere(objectPosition, radius, collidableLayer);

        if (collisions.Length == 1)
        {
            InteractableObject intObj = collisions[0].GetComponent<InteractableObject>();

            var dirToTarget = objectPosition - transform.position;
            
            if (LayerMask.NameToLayer(intObj.layerTrigger) == gameObject.layer)
            {
                intObj.OnObjectStart();
            }
            else
            {
                EventManager.Trigger("ResetAbility");
            }
        }
    }
}
