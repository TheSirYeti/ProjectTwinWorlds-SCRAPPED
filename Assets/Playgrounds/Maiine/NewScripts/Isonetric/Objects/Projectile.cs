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

    void Update()
    {
        actualMovement();
    }

    public void SetPlayer(Player myPlayer)
    {
        _myPlayer = myPlayer;
    }

    public void StartShoot(Vector3 actualObjective)
    {
        transform.forward = (actualObjective - transform.position);
        actualMovement = MoveForward;
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        actualMovement = delegate { };

        IWeaponInteractable actualIWeapon = other.gameObject.GetComponent<IWeaponInteractable>();
        if(actualIWeapon != null)
        {
            transform.parent = other.transform;
            myShootingController.SetConnectObject();
            actualIWeapon.DoWeaponAction(_myPlayer, isPentadent);
        }
    }
}
