using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public delegate void Movement();
    public Movement actualMovement;

    Climb _actualClimb;
    Transform _grabPoint;
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

    public MovementController(Transform playerTransform, Rigidbody playerRigidbody, float speed, LayerMask layerMask,float climbSpeed, Transform lookAtPoint, CameraController cameraController)
    {
        _playerTransform = playerTransform;
        _playerRigidbody = playerRigidbody;
        _cameraController = cameraController;
        _speed = speed;
        _climbSpeed = climbSpeed;
        actualMovement = delegate { };
        lookAtItem = lookAtPoint;
        _layerMask = layerMask;
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

        if (_playerTransform.localPosition.x >= _grabPoint.transform.position.x + 2 || 
            _playerTransform.localPosition.x <= _grabPoint.transform.position.x - 2)
        {
            _actualClimb.MoveGrapPoint(new Vector3(_dir.x, 0, 0));
        }
        else
        {
            Vector3 newDir = (_grabPoint.right * _dir.x * _speed);
            _playerTransform.localPosition += newDir * Time.deltaTime;
        }

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
