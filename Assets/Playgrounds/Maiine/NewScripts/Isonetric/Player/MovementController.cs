using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    public delegate void Movement();
    public Movement actualMovement;

    Swing _actualSwing;
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

    float _maxForce = 40;
    float _actualForce = 0;
    float _deacelerationForce = 30;
    Vector3 _forceDir = Vector3.zero;

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
        Force();
        _playerTransform.position += _dir * _speed * Time.deltaTime;
    }

    void Swing()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_playerTransform.position, 1, _layerMask);

        if (hitColliders.Length > 0 && _dir.z < 0)
        {
            _dir.z = 0;
        }

        //_playerTransform.up = _actualSwing.transform.position - _player.transform.position;
        _player.transform.position += (_actualSwing.transform.position - _player.transform.position) * _dir.z * Time.deltaTime;

        _actualSwing.SetDir(_dir.x);
    }

    void Force()
    {
        _actualForce = Mathf.Clamp(_actualForce, 0, _maxForce);

        _playerTransform.position += _forceDir * _actualForce * Time.deltaTime;
        _actualForce -= _deacelerationForce * Time.deltaTime;
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

        _actualClimb.MoveGrapPoint(_dir.x);

        Vector3 verticalDir = (_grabPoint.up * _dir.z * _climbSpeed);
        _playerTransform.localPosition += verticalDir * Time.deltaTime;
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

    public void SetForce(Vector3 dir, float forcePorcent)
    {
        _actualForce = _maxForce * forcePorcent;
        _forceDir = dir;
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

    public void ChangeToSwing(Swing actualSwing)
    {
        _actualSwing = actualSwing;
        actualMovement = Swing;
        _playerRigidbody.useGravity = false;
    }

    public void ChangeToStay()
    {
        actualMovement = delegate { };
    }
}
