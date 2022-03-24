using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimingBehaviour : MonoBehaviour
{
    private Vector3 lookAtPosition;
    private Vector3 direction;
    [SerializeField] private float distance;
    public Camera cam;

    private Action aimingDelegate;

    private void Start()
    {
        aimingDelegate = GenerateNormalAim;
    }

    private void Update()
    {
        aimingDelegate();
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
   
}
