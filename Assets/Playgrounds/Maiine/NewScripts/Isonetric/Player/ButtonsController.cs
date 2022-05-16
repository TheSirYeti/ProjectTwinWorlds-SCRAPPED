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
    
    public ButtonsController(Player player, MovementController movementController, CameraController cameraController)
    {
        _player = player;
        _movementContoller = movementController;
        _cameraController = cameraController;
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
        MouseButtons();
        InteractableButton();
        ChangePlayerButton();
    }

    void Axies()
    {
        Vector3 myMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        _movementContoller.SetDir(myMovement);
    }

    void MouseButtons()
    {
        if (Input.GetMouseButtonDown(1))
            _cameraController.ChangeAimState(true);
        else if (Input.GetMouseButtonUp(1))
            _cameraController.ChangeAimState(false);
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
