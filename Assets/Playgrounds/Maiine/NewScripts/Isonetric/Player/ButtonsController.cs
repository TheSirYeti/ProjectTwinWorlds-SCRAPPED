using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController
{
    public delegate void Buttons();
    public Buttons actualAxies;
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
        _collisionLayers = player.movementCollision;
        _animatoContoller = player.myAnimatorController;
        actualAxies = delegate { };
        actualButtons = delegate { };
    }

    public void ButtonsOn()
    {
        actualButtons = OnUpdate;
        actualAxies = Axies;
    }

    public void ButtonsOff()
    {
        actualAxies = delegate { };
        actualButtons = delegate { };
    }

    public void ChangeAxies(bool isRaw)
    {
        if (!isRaw)
        {
            actualAxies = Axies;
        }
        else
        {
            actualAxies = RawAxies;
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
        _animatoContoller.SetFloat("MoveMagnitud", myMovement.magnitude);
    }


    void Axies()
    {
        float horizontalAxie = Input.GetAxis("Horizontal");
        float verticalAxie = Input.GetAxis("Vertical");

        _horizontal = _cameraController.transform.right * horizontalAxie;
        _vertical = _cameraController.transform.forward * verticalAxie;

        RayCastCheck();
        Vector3 myMovement = _horizontal + _vertical;

        if (myMovement.magnitude > 1)
            myMovement.Normalize();

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) &&
           !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            myMovement = Vector3.zero;

        _movementContoller.SetDir(myMovement);
        _animatoContoller.SetFloat("MoveMagnitud", myMovement.magnitude);
    }

    void RayCastCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, _horizontal, out hit, 0.5f, _collisionLayers))
        {
            if (CheckList(hit.collider.gameObject, _horizontal))
                _horizontal = Vector3.zero;
        }

        if (Physics.Raycast(_player.transform.position, _vertical, out hit, 0.5f, _collisionLayers))
        {
            if (CheckList(hit.collider.gameObject, _vertical))
                _vertical = Vector3.zero;
        }
    }

    bool CheckList(GameObject collider, Vector3 dir)
    {
        foreach (GameObject skipItems in _player.ignoreGO)
        {
            if (skipItems == collider)
            {
                if (!Physics.Raycast(collider.transform.position, dir, 1f, _collisionLayers))
                    return false;
                else
                    return true;
            }
        }
        return true;
    }

    void AimButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _cameraController.ChangeAimState(true);
            isAiming = true;
            _movementContoller.SetAim(isAiming);
            _animatoContoller.ChangeBool("IsAiming", isAiming);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _cameraController.ChangeAimState(false);
            isAiming = false;
            _movementContoller.SetAim(isAiming);
            _animatoContoller.ChangeBool("IsAiming", isAiming);
        }
    }

    void ShootButton()
    {
        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            if (_animatoContoller.CheckAnimationState("Ani_AimIdle"))
                _animatoContoller.SetTrigger("Shoot");

            _shootingController.Shoot();
        }
    }

    void InteractableButton()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _player.CheckInteractable(KeyCode.F);
        else if (Input.GetKeyDown(KeyCode.Space))
            _player.CheckInteractable(KeyCode.Space);
    }

    void ChangePlayerButton()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventManager.Trigger("OnPlayerChange");
            EventManager.Trigger("ChangePlayer");
        }
    }
}
