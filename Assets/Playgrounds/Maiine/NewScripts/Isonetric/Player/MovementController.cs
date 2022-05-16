using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public delegate void Movement();
    public Movement actualMovement;

    Transform _playerTransform;
    float _speed;

    Vector3 _dir;

    public MovementController(Transform playerTransform, float speed)
    {
        _playerTransform = playerTransform;
        _speed = speed;
        actualMovement = delegate { };
    }

    void InputMovement()
    {
        _playerTransform.position += _dir * _speed * Time.deltaTime;
    }

    void Swing()
    {

    }

    void Force()
    {

    }

    public void SetDir(Vector3 movementVector)
    {
        _dir = movementVector;
    }

    public void ChangeToMove()
    {
        actualMovement = InputMovement;
    }

    public void ChangeToStay()
    {
        actualMovement = delegate { };
    }
}
