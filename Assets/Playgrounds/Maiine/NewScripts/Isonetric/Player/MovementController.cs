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
        if (Physics.Raycast(_playerTransform.position, _playerTransform.forward, 0.5f, collisionLayers) && _dir.z > 0)
        {
            _dir.z = 0;
        }

        if (Physics.Raycast(_playerTransform.position, _playerTransform.forward * -1, 0.5f, collisionLayers) && _dir.z < 0)
        {
            _dir.z = 0;
        }

        if (Physics.Raycast(_playerTransform.position, _playerTransform.right, 0.5f, collisionLayers) && _dir.x > 0)
        {
            _dir.x = 0;
        }

        if (Physics.Raycast(_playerTransform.position, _playerTransform.right * -1, 0.5f, collisionLayers) && _dir.x < 0)
        {
            _dir.x = 0;
        }


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
