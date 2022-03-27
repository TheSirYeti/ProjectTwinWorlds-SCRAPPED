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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            weapon.transform.position = hit.point;
            weapon.transform.LookAt(transform.forward);
        }
    }

    public override void ThrowAbility()
    {
        throw new System.NotImplementedException();
    }
}
