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
    AnimatorController _animatoContoller;

    bool isAiming = false;
    bool axiesRaw = false;

    private Vector3 _horizontal;
    private Vector3 _vertical;

    LayerMask _collisionLayers;

    public ButtonsController(Player player)
    {
        _player = player;
        _movementContoller = player.myMovementController;
        _cameraController = player.cameraController;
        _shootingController = player.myShootingController;
        _collisionLayers = player.collisionMask;
        _animatoContoller = player.myAnimatorController;
        actualButtons = delegate { };
    }

    public void ButtonsOn()
    {
        actualButtons += OnUpdate;
        actualButtons += Axies;
    }

    public void ButtonsOff()
    {
        actualButtons = delegate { };
    }

    public void ChangeAxies()
    {
        if (axiesRaw)
        {
            actualButtons -= RawAxies;
            actualButtons += Axies;
            axiesRaw = false;
        }
        else
        {
            actualButtons += RawAxies;
            actualButtons -= Axies;
            axiesRaw = true;
        }
    }

    public void OnUpdate()
    {
        AimButton();
        InteractableButton();
        ChangePlayerButton();
        ShootButton();
    }

    void RawAxies()
    {
        float horizontalAxie = Input.GetAxis("Horizontal");
        float verticalAxie = Input.GetAxis("Vertical");


        Vector3 myMovement = new Vector3(horizontalAxie, 0, verticalAxie);
        _movementContoller.SetDir(myMovement);
        _animatoContoller.SetFloat("MovementMagnitud", myMovement.magnitude);
    }

    void Axies()
    {
        float horizontalAxie = Input.GetAxis("Horizontal");
        float verticalAxie = Input.GetAxis("Vertical");

        Debug.Log("oa?");
        Debug.Log(_cameraController.transform.right);

        _horizontal = _cameraController.transform.right * horizontalAxie;
        _vertical = _cameraController.transform.forward * verticalAxie;

        RayCastCheck();
        Vector3 myMovement = _horizontal + _vertical;

        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        _movementContoller.SetDir(myMovement);
        _animatoContoller.SetFloat("MovementMagnitud", myMovement.magnitude);
    }

    void RayCastCheck()
    {
        if (Physics.Raycast(_player.transform.position, _horizontal, 0.5f, _collisionLayers))
        {
            _horizontal = Vector3.zero;
        }

        if (Physics.Raycast(_player.transform.position, _vertical, 0.5f, _collisionLayers))
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
            _movementContoller.SetAim(isAiming);
            _animatoContoller.ChangeBool("Aim", isAiming);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _cameraController.ChangeAimState(false);
            isAiming = false;
            _movementContoller.SetAim(isAiming);
            _animatoContoller.ChangeBool("Aim", isAiming);
        }
    }

    void ShootButton()
    {
        if (Input.GetMouseButtonDown(0) && isAiming)
            _shootingController.Shoot();
    }

    void InteractableButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
