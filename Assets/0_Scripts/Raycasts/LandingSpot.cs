using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LandingSpot : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] float radius = 1f;
    [SerializeField] float maxDistance = 30f;

    private GameObject circle;

    private void Start()
    {
        SetPrefabValues();
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance))
        {
            circle.transform.position = hit.point;
            circle.transform.position += Vector3.up * 0.01f;
        }
    }

    void SetPrefabValues()
    {
        circle = Instantiate(circlePrefab);
        circle.transform.localScale = new Vector3(radius, 1, radius);
    }
}
