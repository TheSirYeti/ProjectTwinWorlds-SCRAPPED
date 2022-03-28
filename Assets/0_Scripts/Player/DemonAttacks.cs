using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonAttacks : PlayerAttacks
{
    public GameObject weapon;
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
            weapon.transform.forward = transform.position - hit.point;
            StartCoroutine(ThrowPentadent(flightDuration, hit.point));
        }
    }

    IEnumerator ThrowPentadent(float duration, Vector3 destiny)
    {
        weapon.GetComponent<BoxCollider>().enabled = false;
        float time = 0;
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            weapon.transform.position = Vector3.Lerp(transform.position, destiny, time / 2);
            if (duration % time >= 0.5f)
            {
                weapon.GetComponent<BoxCollider>().enabled = true;
            }

            if (Vector3.Distance(weapon.transform.position, destiny) <= 0.25)
            {
                SearchForParent();
            }
            yield return new WaitForSeconds(0.005f);
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
        weapon.transform.SetParent(null);
        weapon.gameObject.SetActive(false);
    }
}
