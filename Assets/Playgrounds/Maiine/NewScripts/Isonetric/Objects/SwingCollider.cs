using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingCollider : MonoBehaviour
{
    Player _actualPlayer;
    public Swing mySwing;
    public bool rigth;
    public bool mid;

    private void OnTriggerEnter(Collider other)
    {
        _actualPlayer = other.GetComponent<Player>();


        if(_actualPlayer != null)
        {
            if (!mid)
            {
                mySwing.SetBackState(rigth, true);
            }
            else if (mid)
            {
                mySwing.CancelMovement();
            }
        }
    }
}
