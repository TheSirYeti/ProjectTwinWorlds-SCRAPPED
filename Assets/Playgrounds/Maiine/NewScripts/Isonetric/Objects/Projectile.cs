using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    delegate void ProjectileDelegate();
    ProjectileDelegate actualMovement = delegate { };

    public float speed;

    public ShootingController myShootingController;

    void Update()
    {
        actualMovement();
    }

    public void StartShoot(Vector3 actualObjective)
    {
        transform.forward = (actualObjective - transform.position);
        Debug.Log(actualObjective);
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
            myShootingController.SetConnectObject();
        }
    }
}
