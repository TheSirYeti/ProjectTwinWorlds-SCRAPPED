using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    [Header("Starting Point")] [SerializeField]
    private bool isOrigin;

    private bool isBeingLasered = false;
    
    
    [Header("Line Renderer")]
    [SerializeField] private LineRenderer lineRenderer;
    
    [Header("Stats Settings")]
    [SerializeField] private Transform startingPoint;
    [SerializeField] private LayerMask collidableLayers;
    [SerializeField] private float maxDistance;
    [SerializeField] private float detectionRadius;

    private LaserLogic currentLaser;
    private LaserReceptor currentReceptor;

    private void Update()
    {
        if ((isBeingLasered || isOrigin) && lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, startingPoint.position);
        
            RaycastHit hit;

            if (Physics.Raycast(startingPoint.position, startingPoint.forward, out hit, maxDistance, collidableLayers))
            {
                lineRenderer.SetPosition(1, hit.point);
                CheckForLaserHit(hit.point);
            }
            else
            {
                lineRenderer.SetPosition(1, startingPoint.position + (startingPoint.forward * maxDistance));
            }
        }
    }

    public void SetLaserStatus(bool value)
    {
        isBeingLasered = value;
        lineRenderer.enabled = value;
    }

    public void CheckForLaserHit(Vector3 hitPos)
    {
        Collider[] itemsCollided = Physics.OverlapSphere(hitPos, detectionRadius, collidableLayers);

        int specialInteractionCounter = 0;

        foreach (var collider in itemsCollided)
        {
            if (collider.GetComponent<LaserLogic>() != null || collider.GetComponent<LaserReceptor>() != null)
            {
                specialInteractionCounter++;
            }
        }
        
        if (specialInteractionCounter == 0)
        {
            DisableCurrentRelations();
            return;
        }

        foreach (var collider in itemsCollided)
        {
            if (collider.GetComponent<LaserLogic>() != null)
            {
                currentLaser = collider.GetComponent<LaserLogic>();
                currentLaser.SetLaserStatus(true);
            }

            if (collider.GetComponent<LaserReceptor>() != null)
            {
                currentReceptor = collider.GetComponent<LaserReceptor>();
                currentReceptor.SetRecieverStatus(true);
            }
        }
    }

    public void DisableCurrentRelations()
    {
        if (currentLaser != null)
        {
            currentLaser.SetLaserStatus(false);
            currentLaser.DisableCurrentRelations();
            currentLaser = null;
        }

        if (currentReceptor != null)
        {
            currentReceptor.SetRecieverStatus(false);
            currentReceptor = null;
        }
    }

}
