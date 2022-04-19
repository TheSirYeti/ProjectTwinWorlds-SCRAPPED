using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemsLvl;
    [SerializeField] Material BaseMat;
    [SerializeField] Material OpacityMat;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            foreach (var item in itemsLvl)
            {
                item.gameObject.GetComponent<Renderer>().material = OpacityMat;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            foreach (var item in itemsLvl)
            {
                item.gameObject.GetComponent<Renderer>().material = BaseMat;
            }
        }
    }

}
