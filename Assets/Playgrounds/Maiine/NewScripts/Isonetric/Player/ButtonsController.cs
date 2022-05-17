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

    public ButtonsController(Player player, MovementController movementController, CameraController cameraController, ShootingController shootingController)
    {
        _player = player;
        _movementContoller = movementController;
        _cameraController = cameraController;
        _shootingController = shootingController;
        actualButtons = delegate { };
    }

    public void ButtonsOn()
    {
        Debug.Log("active");
        actualButtons = OnUpdate;
    }

    public void ButtonsOff()
    {
        Debug.Log("off");
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
        Vector3 myMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        _movementContoller.SetDir(myMovement);
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
