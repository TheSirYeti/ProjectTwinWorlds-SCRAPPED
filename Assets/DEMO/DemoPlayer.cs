using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    private Vector3 _vMovement;
    [SerializeField] private float _speed = 2;

    private bool _isFighting = false;

    // Update is called once per frame
    void Update()
    {
        _vMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position += _vMovement * _speed * Time.deltaTime;
    }
}
