using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemonAttacks : PlayerAttacks
{
    public GameObject weapon;
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            weapon.transform.LookAt(transform.forward);
            StartCoroutine(ThrowPentadent(2f, hit.point));
        }
    }

    IEnumerator ThrowPentadent(float duration, Vector3 destiny)
    {
        float time = 0;
        while (time <= duration)
        {
            time += Time.fixedDeltaTime;
            weapon.transform.position = Vector3.Lerp(transform.position, destiny, time / 2);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public override void ThrowAbility()
    {
        weapon.gameObject.SetActive(false);
    }
}
