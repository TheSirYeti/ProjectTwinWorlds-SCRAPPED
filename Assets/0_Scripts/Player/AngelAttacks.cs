using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAttacks : PlayerAttacks
{
    public GameObject weapon;
    public LayerMask pentadenteMask;
    public LineRenderer lineRenderer;
    public bool isConnected;
    
    private void FixedUpdate()
    {
        if (isConnected)
        {
            lineRenderer.SetPosition(0, transform.position);
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
            isConnected = true;
        }
    }

    public override void ThrowAbility()
    {
        isConnected = false;
        lineRenderer.enabled = false;
    }
}
