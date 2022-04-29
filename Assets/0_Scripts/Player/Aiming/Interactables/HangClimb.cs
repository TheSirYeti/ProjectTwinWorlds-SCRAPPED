using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HangClimb : InteractableObject
{
    public Transform heightPoint, angel;
    public GameObject walls;
    public List<Transform> startPoints;
    public bool isOnLeft;
    
    
    public override void OnObjectStart()
    {
        angel = PlayerWorlds.instance.angelPlayer.transform;
        angel.position = startPoints[GetNearestPoint(angel)].position;
        walls.SetActive(true);
        EventManager.Trigger("OnClimbStart", isOnLeft);
        isObjectTriggered = true;
    }

    public override void OnObjectDuring()
    {
        if (angel.position.y >= heightPoint.position.y)
        {
            angel.position = new Vector3(angel.position.x, heightPoint.position.y, angel.position.z);
        }
    }

    public override void OnObjectEnd()
    {
        walls.SetActive(false);
        ResetVariables(null);
        Debug.Log("Loop check 3 - Hang");
        EventManager.Trigger("OnClimbStop");
        EventManager.Trigger("ResetAbility");
    }

    public override void OnObjectExecute()
    {
        throw new NotImplementedException();
    }

    public int GetNearestPoint(Transform myTransform)
    {
        int currentId = -1;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < startPoints.Count; i++)
        {
            float distance = Vector3.Distance(myTransform.position, startPoints[i].position);

            if (distance <= minDistance)
            {
                currentId = i;
                minDistance = distance;
            }
        }

        return currentId;
    }
}
