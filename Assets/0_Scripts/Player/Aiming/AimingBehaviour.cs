using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimingBehaviour : MonoBehaviour, ISubscriber
{
    private Vector3 lookAtPosition;
    private Vector3 direction;
    [SerializeField] private float distance;
    public Camera cam;
    public Observer _playerObserver;
    
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

    public void OnNotify(string eventID)
    {
        if (eventID == "BasicAttack")
        {
            GenerateNormalAim();
        }
    }
}
