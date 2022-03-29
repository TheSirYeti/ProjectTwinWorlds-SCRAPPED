using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingProperties : MonoBehaviour
{
    public Transform landingPosition1, landingPosition2;

    public Transform GetFurthestLandingPosition(Transform transform)
    {
        if (Vector3.Distance(transform.position, landingPosition1.position) >=
            Vector3.Distance(transform.position, landingPosition2.position))
        {
            return landingPosition1;
        }
        else return landingPosition2;
    }
    
}
