using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    delegate void ProjectileDelegate();
    ProjectileDelegate actualMovement = delegate { };

    public float speed;

    void Update()
    {
        actualMovement();
    }

    public void StartShoot(Transform actualObjective)
    {
        transform.LookAt(actualObjective);
        actualMovement = MoveForward;
    }

    void MoveForward()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        actualMovement = delegate { };
    }
}
