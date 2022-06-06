using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingCollider : MonoBehaviour
{
    public Swing mySwing;
    public bool usableByDemon;

    private void OnTriggerEnter(Collider other)
    {
        Player _actualPlayer = other.GetComponent<Player>();

        if(_actualPlayer != null && _actualPlayer.isDemon == usableByDemon)
        {
            mySwing.canSwing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player _actualPlayer = other.GetComponent<Player>();

        if (_actualPlayer != null && _actualPlayer.isDemon == usableByDemon)
        {
            mySwing.canSwing = false;
        }
    }
}
