using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JailController : MonoBehaviour
{
    public BirdController bird;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null && Input.GetKeyDown(KeyCode.E))
        {
            bird.OpenDoor();
            Debug.Log("aca");
        }
            
    }
}
