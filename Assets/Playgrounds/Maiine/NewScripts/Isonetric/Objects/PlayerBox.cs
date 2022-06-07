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
        {
            Debug.Log("aca");
            StopParent();
        }
    }

    public void Inter_DoJumpAction(Player actualPlayer, bool isDemon)
    {
        if (!isDemon) return;


        if (actualPlayer.transform.position.y <= transform.position.y)
            actualPlayer.transform.position = climbPoint.position;
    }

    public void Inter_DoPlayerAction(Player actualPlayer, bool isDemon)
    {
        Debug.Log("a");
        if (!isDemon) return;
        Debug.Log("b");

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
        myBox.GetComponent<MovableBox>().isCaught = true;
        isParent = true;
        Rigidbody actualBoxRigidbody = myBox.GetComponent<Rigidbody>();
        actualBoxRigidbody.Sleep();
        _actualPlayer.transform.forward = transform.position - _actualPlayer.transform.position;
        myBox.transform.parent = _actualPlayer.gameObject.transform;
        _actualPlayer.AddIgnore(myBox);
        _actualPlayer.AddIgnore(this.gameObject);
        isOnPlayer = true;
    }

    void StopParent()
    {
        myBox.GetComponent<MovableBox>().isCaught = false;
        isParent = false;
        myBox.GetComponent<Rigidbody>().WakeUp();
        myBox.transform.parent = null;
        _actualPlayer.RemoveIgnore(myBox);
        _actualPlayer.RemoveIgnore(this.gameObject);
        isOnPlayer = false;
    }

}
