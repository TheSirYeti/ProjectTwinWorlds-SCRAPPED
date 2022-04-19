
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FederController : MonoBehaviour
{
    public bool haveFood = false;
    public bool isInPosition = false;
    public Renderer rend;
    public Material full;
    public string tagPos;

    public bool HaveFood()
    {
        if (isInPosition && haveFood)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Feder")
        {
            haveFood = true;
            rend.material = full;
        }

        if (other.gameObject.tag == tagPos)
        {
            isInPosition = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagPos)
        {
            isInPosition = false;
        }
    }
}
