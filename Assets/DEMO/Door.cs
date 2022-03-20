using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool button1;
    public bool button2;
    public bool activated;

    private void Update()
    {
        if (button1 && button2 && !activated)
        {
            activated = true;
            transform.position += Vector3.up * 3;
        }
    }
}
