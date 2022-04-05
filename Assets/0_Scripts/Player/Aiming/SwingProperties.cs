using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwingProperties : MonoBehaviour
{
    public List<Transform> swingPositions;

    public int GetNearestLandingPosition(Transform  transformTarget)
    {
        float minDistance = Mathf.Infinity;
        int nearestId = -1;
        
        for (int i = 0; i < swingPositions.Count; i++)
        {
            var currentDistance = Vector3.Distance(transformTarget.position, swingPositions[i].position);
            if (currentDistance <= minDistance)
            {
                minDistance = currentDistance;
                nearestId = i;
            }
        }

        return nearestId;
    }

    public int DetermineOrientation(int positionId)
    {
        float total = positionId / swingPositions.Count - 1;

        if (total > 0.5f)
        {
            return 1;
        }
        else return -1;
    }

}
