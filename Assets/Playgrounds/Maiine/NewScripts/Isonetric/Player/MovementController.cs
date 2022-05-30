using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public delegate void Movement();
    public Movement actualMovement;

    Climb _actualClimb;
    Transform _grabPoint;

    Player _player;
    CameraController _cameraController;
    Transform _playerTransform;
    Rigidbody _playerRigidbody;
    Transform lookAtItem;
    float _speed;
    float _climbSpeed;
    bool isAim = false;
    bool canUp = true;
    LayerMask _layerMask;

    Vector3 _dir;

    public MovementController(Player player)
    {
        _player = player;
        _playerTransform = _player.transform;
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _cameraController = _player.cameraController;
        _speed = _player.speed;
        _climbSpeed = _player.climbSpeed;
        actualMovement = delegate { };
        lookAtItem = _player.lookAtPoint;
        _layerMask = _player.movementCollision;
    }

    void InputMovement()
    {
        LookAt();
        _playerTransform.position += _dir * _speed * Time.deltaTime;
    }

    void Swing()
    {

    }

    void Climb()
    {
        if (Physics.Raycast(_playerTransform.position, Vector3.down, 1f, _layerMask) && _dir.z < 0)
        {
            _dir.z = 0;
        }

        if (!canUp && _dir.z > 0)
        {
            _dir.z = 0;
        }

        _actualClimb.MoveGrapPoint(new Vector3(_dir.x, 0, 0));

        Vector3 verticalDir = (_grabPoint.up * _dir.z * _climbSpeed);
        _playerTransform.localPosition += verticalDir * Time.deltaTime;
    }

    void Force()
    {

    }

    void LookAt()
    {
        if (!isAim)
        {
            lookAtItem.position = _playerTransform.position + _dir;
            Debug.Log("this?");
        }
        else
        {
            lookAtItem.position = _cameraController.midCamera;
        }
        _playerTransform.LookAt(lookAtItem);
    }

    public void SetAim(bool state)
    {
        isAim = state;
    }

    public void SetUp(bool state)
    {
        canUp = state;
    }

    public void SetDir(Vector3 movementVector)
    {
        _dir = movementVector;
    }

    public void ChangeToMove()
    {
        _playerRigidbody.useGravity = true;
        actualMovement = InputMovement;
    }

    public void ChangeToClimb(Climb actualClimb, Transform grabPoint)
    {
        _grabPoint = grabPoint;
        _actualClimb = actualClimb;
        actualMovement = Climb;
        _playerRigidbody.useGravity = false;
    }

    public void ChangeToStay()
    {
        actualMovement = delegate { };
    }
}
