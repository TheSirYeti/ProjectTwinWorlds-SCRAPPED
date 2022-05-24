using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    delegate void ProjectileDelegate();
    ProjectileDelegate actualMovement = delegate { };

    public float speed;
    public bool isPentadent;

    public ShootingController myShootingController;

    Player _myPlayer;
    GameObject goingTo;

    void Update()
    {
        actualMovement();
    }

    public void SetPlayer(Player myPlayer)
    {
        _myPlayer = myPlayer;
    }

    public void StartShoot(Vector3 actualObjective, GameObject objectGoTo)
    {
        transform.forward = (actualObjective - transform.position);
        goingTo = objectGoTo;
        actualMovement = MoveForward;
    }

    public void StopShoot()
    {
        actualMovement = delegate { };
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        IWeaponInteractable actualObject = other.gameObject.GetComponent<IWeaponInteractable>();
        if (other.gameObject == goingTo)
        {
            actualMovement = delegate { };
            myShootingController.SetConnectObject();

            if (actualObject != null)
            {
                actualObject.SetWeaponState(true);
                actualObject.Interact(_myPlayer, isPentadent, this, myShootingController);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IWeaponInteractable actualObject = other.gameObject.GetComponent<IWeaponInteractable>();

        if (actualObject != null)
        {
            actualObject.SetWeaponState(false);
        }

    }
}
