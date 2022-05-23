using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController
{
    const int maxIterations = 100;
    const int minAcceptableDist = 100;

    public Vector3[] Solve(Vector3[] points, Vector3 target)
    {
        Vector3 origin = points[0];
        float[] segmentLengths = new float[points.Length - 1];

        for (int i = 0; i < points.Length - 1; i++)
        {
            segmentLengths[i] = (points[i + 1] - points[i]).magnitude;
        }

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            bool startingFromTarget = iteration % 2 == 0;

            System.Array.Reverse(points);
            System.Array.Reverse(segmentLengths);
            points[0] = (startingFromTarget) ? target : origin;

            for (int i = 0; i < points.Length; i++)
            {
                if (i >= 1)
                {
                    Vector3 dir = (points[i] - points[i - 1]).normalized;
                    points[i] = points[i - 1] + dir * segmentLengths[i - 1];

                }
            }

            float distToTarget = (points[points.Length - 1] - target).magnitude;

            if (!startingFromTarget && distToTarget <= minAcceptableDist)
                return points;
        }

        return points;
    }
}
