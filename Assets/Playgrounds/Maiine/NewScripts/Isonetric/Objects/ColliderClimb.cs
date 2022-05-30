using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderClimb : MonoBehaviour
{
    Player _actualPlayer;
    public Climb myClimb;
    public GameObject myGide;
    public bool rigth;
    public bool down;

    private void OnTriggerEnter(Collider other)
    {
        _actualPlayer = other.GetComponent<Player>();

        if (!down)
        {
            if (other.gameObject == myGide)
            {
                myClimb.SetMovement(rigth, true);
            }
        }
        else if(down && _actualPlayer != null)
        {
            _actualPlayer.myMovementController.SetUp(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!down)
        {
            if (other.gameObject == myGide)
            {
                myClimb.SetMovement(rigth, false);
            }
        }
        else if (down && _actualPlayer != null)
        {
            _actualPlayer.myMovementController.SetUp(true);
            _actualPlayer = null;
        }
    }
}
