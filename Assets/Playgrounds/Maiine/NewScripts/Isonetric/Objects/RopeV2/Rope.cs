using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform[] points;
    public Vector3[] vectorPoints;
    RopeController ropeController;
    public float distanceBetweenPoints;
    public int maxIterations;
    public Transform target;
    public Transform start;
    public LineRenderer lineRenderer;

    public void Start()
    {
        lineRenderer.positionCount = points.Length;
        ropeController = new RopeController();
        vectorPoints = new Vector3[points.Length];
        int count = 0;
        foreach (Transform point in points)
        {
            vectorPoints[count] = point.position;
            count++;
        }
    }

    void Update()
    {
        //vectorPoints[0] = start.position;
        vectorPoints = ropeController.Solve(vectorPoints, target.position);

        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, vectorPoints[i]);
        }
    }
}
