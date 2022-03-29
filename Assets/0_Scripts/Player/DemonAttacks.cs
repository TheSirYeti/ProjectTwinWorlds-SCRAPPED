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

    public override void ThrowAbility(object[] parameters)
    {
        Debug.Log(weapon.gameObject);
        weapon.StopCoroutine(weapon.ThrowPentadent());
        weapon.transform.SetParent(null);
        weapon.transform.position = transform.position;
        weapon.foundParent = false;
        weapon.gameObject.SetActive(false);
    }
}
