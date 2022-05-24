using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float climbSpeed;
    public bool isActive;
    public bool isDemon;

    [HideInInspector]
    public int actualRoom;

    public Projectile pentadentPrefab;
    public Projectile arrowPrefab;

    public GameObject realCharacter;
    public GameObject totemCharacter;

    CapsuleCollider myCollider;
    Rigidbody myRigidbody;
    public Animator myAnimator;
    public Transform lookAtPoint;

    IPlayerInteractable _playerInteractable = null;

    [HideInInspector]
    public ShootingController myShootingController;
    [HideInInspector]
    public MovementController myMovementController;
    [HideInInspector]
    public ButtonsController myButtonController;
    [HideInInspector]
    public AnimatorController myAnimatorController;
    public CameraController cameraController;

    public LayerMask usableItems;
    public LayerMask collisionMask;

    private void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
        myRigidbody = GetComponent<Rigidbody>();


        Projectile actualProjectil;

        if (isDemon)
            actualProjectil = Instantiate(pentadentPrefab, new Vector3(0, -50, 0), Quaternion.Euler(Vector3.zero));
        else
            actualProjectil = Instantiate(arrowPrefab, new Vector3(0, -50, 0), Quaternion.Euler(Vector3.zero));

        actualProjectil.SetPlayer(this);
        myShootingController = new ShootingController(actualProjectil, this, usableItems, isDemon);

        myMovementController = new MovementController(transform, myRigidbody, speed, collisionMask, climbSpeed, lookAtPoint, cameraController);
        myAnimatorController = new AnimatorController(myAnimator);
        myButtonController = new ButtonsController(this);

        EventManager.Subscribe("ChangePlayer", ChangeCharacter);

        if (isActive)
            StartCoroutine(CorrutinaTurnOn());
        else
            TurnOff();
    }

    void Update()
    {
        myButtonController.actualButtons();
        myMovementController.actualMovement();
    }


    public void CheckInteractable()
    {
        if (_playerInteractable == null) return;

        _playerInteractable.DoPlayerAction(this, isDemon);
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
