using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    private Vector3 _vMovement;
    public Transform direction;
    [SerializeField] private float _speed = 2;

    private bool _isFighting = false;

    // Update is called once per frame
    void Update()
    {
        _vMovement = Input.GetAxis("Horizontal") * direction.right + Input.GetAxis("Vertical") * direction.forward;

        transform.position += _vMovement * _speed * Time.deltaTime;

        //NT
    }
}
