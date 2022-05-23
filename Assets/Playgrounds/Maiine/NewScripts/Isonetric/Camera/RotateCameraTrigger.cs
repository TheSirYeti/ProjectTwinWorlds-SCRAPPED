using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraTrigger : MonoBehaviour
{
    [Header("PARA QUE LADO QUERES QUE ROTE")]
    public bool rotateRigth;

    [Header("SIEMPRE NUMEROS POSITIVOS, ROTACION 360")]
    public float angleToRotate;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.cameraController.SetRotate(rotateRigth, angleToRotate);
        }
    }
}
