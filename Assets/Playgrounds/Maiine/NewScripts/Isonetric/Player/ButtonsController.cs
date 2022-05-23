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

    private Vector3 horizontal;
    private Vector3 vertical;

    LayerMask collisionLayers;

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
        horizontal = _cameraController.transform.right * Input.GetAxis("Horizontal");
        vertical = _cameraController.transform.forward * Input.GetAxis("Vertical");

        Raycast(vertical, horizontal);
        Vector3 myMovement = _cameraController.transform.right * Input.GetAxis("Horizontal") + _cameraController.transform.forward * Input.GetAxis("Vertical");


        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        _movementContoller.SetDir(myMovement);
    }

    void Raycast(Vector3 z, Vector3 x)
    {
        if (Physics.Raycast(_player.transform.position, z, 0.5f, collisionLayers))
        {
            horizontal = Vector3.zero;
        }

        if(Physics.Raycast(_player.transform.position, x, 0.5f, collisionLayers))
        {

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
