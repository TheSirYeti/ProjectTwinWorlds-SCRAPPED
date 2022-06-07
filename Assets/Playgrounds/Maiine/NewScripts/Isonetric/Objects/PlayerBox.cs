using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBox : MonoBehaviour, IPlayerInteractable
{
    bool isOnPlayer = false;

    public GameObject myBox;
    public Transform climbPoint;
    public float minDistanceToStay;
    Player _actualPlayer;
    bool isParent;

    private void Update()
    {
        if (_actualPlayer != null && Vector3.Distance(_actualPlayer.transform.position, transform.position) > minDistanceToStay && isParent)
            StopParent();
    }

    public void Inter_DoJumpAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;


        if (actualPlayer.transform.position.y <= transform.position.y)
            actualPlayer.transform.position = climbPoint.position;
    }

    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;

        if (!isOnPlayer)
        {
            _actualPlayer = actualPlayer;
            StartParent();
        }
        else
        {
            StopParent();
        }
    }

    void StartParent()
    {
        isParent = true;
        myBox.GetComponent<Rigidbody>().useGravity = false;
        myBox.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _actualPlayer.transform.forward = transform.position - _actualPlayer.transform.position;
        myBox.transform.parent = _actualPlayer.gameObject.transform;
        _actualPlayer.AddIgnore(myBox);
        _actualPlayer.AddIgnore(this.gameObject);
        isOnPlayer = true;
    }

    void StopParent()
    {
        isParent = false;
        myBox.GetComponent<Rigidbody>().useGravity = true;
        myBox.transform.parent = null;
        _actualPlayer.RemoveIgnore(myBox);
        _actualPlayer.RemoveIgnore(this.gameObject);
        isOnPlayer = false;
    }

}
