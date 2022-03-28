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
        _playerObserver.NotifySubscribers("BasicAttack");
    }

    public override void AimAbility()
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

    public void SearchForParent()
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
            weapon.transform.SetParent(filteredColliders[0].transform);
        }
    }
    
    public override void ThrowAbility(object[] parameters)
    {
        weapon.StopCoroutine(weapon.ThrowPentadent());
        weapon.transform.SetParent(null);
        weapon.transform.position = transform.position;
        weapon.gameObject.SetActive(false);
    }
}
