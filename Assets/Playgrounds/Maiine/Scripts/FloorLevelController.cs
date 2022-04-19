using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLevelController : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemsLvl;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach (var item in itemsLvl)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var item in itemsLvl)
            {
                item.gameObject.SetActive(true);
            }
        }
    }

}
