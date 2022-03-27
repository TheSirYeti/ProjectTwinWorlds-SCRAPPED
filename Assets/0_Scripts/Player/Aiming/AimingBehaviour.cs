using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimingBehaviour : MonoBehaviour, ISubscriber
{
    [SerializeField] private float distance;
    public Camera cam;
    public Observer _playerObserver;
    public LayerMask wallLayer;
    
    private Action aimingDelegate;

    private void Start()
    {
        aimingDelegate = GenerateNormalAim;
        _playerObserver.Subscribe(this);
    }

    void GenerateNormalAim()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }
    
    void GenerateAbilityAim()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, wallLayer))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    public void OnNotify(string eventID)
    {
        if (eventID == "BasicAttack")
        {
            GenerateNormalAim();
        }

        if (eventID == "ThrowAbility")
        {
            GenerateAbilityAim();
        }
    }
}
