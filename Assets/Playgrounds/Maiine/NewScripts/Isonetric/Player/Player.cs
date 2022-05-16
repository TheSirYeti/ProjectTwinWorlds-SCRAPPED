using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isActive;
    public int actualRoom;

    public GameObject realCharacter;
    public GameObject totemCharacter;

    CapsuleCollider myCollider;
    Rigidbody myRigidbody;

    IPlayerInteractable _playerInteractable = null;

    MovementController myMovementController;
    ButtonsController myButtonController;
    public CameraController cameraController;


    private void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
        myRigidbody = GetComponent<Rigidbody>();

        myMovementController = new MovementController(transform, speed);
        myButtonController = new ButtonsController(this, myMovementController, cameraController);

        EventManager.Subscribe("ChangePlayer", ChangeCharacter);

        if (isActive)
        {
            StartCoroutine(CorrutinaTurnOn());
        }
        else
        {
            TurnOff();
        }
    }

    void Update()
    {
        myButtonController.actualButtons();
        myMovementController.actualMovement();
    }


    public void CheckInteractable()
    {
        if (_playerInteractable == null) return;

        _playerInteractable.DoAction();
    }

    #region Player Change
    public void ChangeCharacter(params object[] parameter)
    {
        if (isActive)
        {
            TurnOff();
        }
        else
        {
            StartCoroutine(CorrutinaTurnOn());
        }

        isActive = !isActive;
    }

    void TurnOff()
    {
        myMovementController.ChangeToStay();
        myButtonController.ButtonsOff();
        realCharacter.SetActive(false);
        totemCharacter.SetActive(true);
        myCollider.isTrigger = true;
        myRigidbody.useGravity = false;
    }

    IEnumerator CorrutinaTurnOn()
    {
        yield return new WaitForEndOfFrame();
        myMovementController.ChangeToMove();
        myButtonController.ButtonsOn();
        realCharacter.SetActive(true);
        totemCharacter.SetActive(false);
        myCollider.isTrigger = false;
        myRigidbody.useGravity = true;
    }
    #endregion

    public void SetOffDelegates()
    {
        myMovementController.ChangeToStay();
        myButtonController.ButtonsOff();
    }

    public void SetOnDelegates()
    {
        myMovementController.ChangeToMove();
        myButtonController.ButtonsOn();
    }

    public void SetActualRoom(int nextRoom)
    {
        actualRoom = nextRoom;
    }

    public int GetActualRoom()
    {
        return actualRoom;
    }

    public void GoToTransform(Vector3 actualTransform)
    {
        transform.position = actualTransform;
    }

    private void OnTriggerEnter(Collider other)
    {
        IPlayerInteractable actualPlayerInteractable = other.gameObject.GetComponent<IPlayerInteractable>();
        if (actualPlayerInteractable != null)
        {
            _playerInteractable = actualPlayerInteractable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPlayerInteractable actualPlayerInteractable = other.gameObject.GetComponent<IPlayerInteractable>();
        if (actualPlayerInteractable != null)
            _playerInteractable = null;
    }

}
