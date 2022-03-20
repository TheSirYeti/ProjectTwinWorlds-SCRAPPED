using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStuff : MonoBehaviour
{
    public Transform notActivated, activated;
    public GameObject button;
    public Door door;
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            button.transform.position = activated.position;
            if (id == 1)
                door.button1 = true;
            if (id == 2)
                door.button2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            button.transform.position = notActivated.position;
            if (id == 1)
                door.button1 = false;
            if (id == 2)
                door.button2 = false;
        }
    }
}
