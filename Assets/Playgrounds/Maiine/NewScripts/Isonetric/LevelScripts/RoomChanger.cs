using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    public Transform goToPoint;

    Player actualPlayer;

    public int nextRoom;

    public void TriggerPlayerTransform()
    {
        actualPlayer.GoToTransform(goToPoint.position);
        actualPlayer.SetActualRoom(nextRoom);
        actualPlayer.SetOnDelegates();
    }

    private void OnTriggerEnter(Collider other)
    {
        actualPlayer = other.gameObject.GetComponent<Player>();
        if (actualPlayer != null)
        {
            EventManager.Trigger("ChangeFadeState", this);
            actualPlayer.SetOffDelegates();
        }
    }

}
