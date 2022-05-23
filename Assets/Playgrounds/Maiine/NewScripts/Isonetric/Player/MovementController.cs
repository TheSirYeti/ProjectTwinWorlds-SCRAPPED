using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public delegate void Movement();
    public Movement actualMovement;

    Transform _playerTransform;
    Rigidbody _playerRigidbody;
    float _speed;

    LayerMask collisionLayers;

    Vector3 _dir;

    public MovementController(Transform playerTransform, Rigidbody playerRigidbody, LayerMask collisionMask,float speed)
    {
        _playerTransform = playerTransform;
        _playerRigidbody = playerRigidbody;
        _speed = speed;
        collisionLayers = collisionMask;
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
