using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController
{
    public delegate void Buttons();
    public Buttons actualButtons;

    Player _player;
    MovementController _movementContoller;
    CameraController _cameraController;
    ShootingController _shootingController;

    bool isAiming = false;

    private Vector3 _horizontal;
    private Vector3 _vertical;

    LayerMask collisionLayers;

    public ButtonsController(Player player, MovementController movementController, CameraController cameraController, ShootingController shootingController, LayerMask maskCollision)
    {
        _player = player;
        _movementContoller = movementController;
        _cameraController = cameraController;
        _shootingController = shootingController;
        collisionLayers = maskCollision;
        actualButtons = delegate { };
    }

    public void ButtonsOn()
    {
        actualButtons = OnUpdate;
    }

    public void ButtonsOff()
    {
        actualButtons = delegate { };
    }

    public void OnUpdate()
    {
        Axies();
        AimButton();
        InteractableButton();
        ChangePlayerButton();
        ShootButton();
    }

    void Axies()
    {
        _horizontal = _cameraController.transform.right * Input.GetAxis("Horizontal");
        _vertical = _cameraController.transform.forward * Input.GetAxis("Vertical");

        RayCastCheck();
        Vector3 myMovement = _horizontal + _vertical;

        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        _movementContoller.SetDir(myMovement);
    }

    void RayCastCheck()
    {
        if (Physics.Raycast(_player.transform.position, _horizontal, 0.5f, collisionLayers))
        {
            _horizontal = Vector3.zero;
        }

        if(Physics.Raycast(_player.transform.position, _vertical, 0.5f, collisionLayers))
        {
            _vertical = Vector3.zero;
        }
    }

    void AimButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _cameraController.ChangeAimState(true);
            isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _cameraController.ChangeAimState(false);
            isAiming = false;
        }
    }

    void ShootButton()
    {
        if (Input.GetMouseButtonDown(0) && isAiming)
            _shootingController.Shoot();
    }

    void InteractableButton()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _player.CheckInteractable();
    }

    void ChangePlayerButton()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventManager.Trigger("ChangePlayer");
        }
    }
}
