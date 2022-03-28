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
            yield return new WaitForSeconds(0.005f);
        }
    }
    
    public override void ThrowAbility(object[] parameters)
    {
        weapon.gameObject.SetActive(false);
    }
}
