using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public float speed;
    public float climbSpeed;
    public bool isActive;
    public bool isDemon;

    public bool isOnAir = true;

    [HideInInspector]
    public int actualRoom;

    public BulletSystem pentadentPrefab;
    public BulletSystem arrowPrefab;

    public GameObject realCharacter;
    public GameObject totemCharacter;

    public Transform handPoint;

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

    LifeController _lifeController;

    public LayerMask usableItems;
    public LayerMask movementCollision;
    public LayerMask mouseCollisions;

    private void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
        myRigidbody = GetComponent<Rigidbody>();


        BulletSystem actualProjectil;

        if (isDemon)
            actualProjectil = Instantiate(pentadentPrefab, new Vector3(0, -50, 0), Quaternion.Euler(Vector3.zero));
        else
            actualProjectil = Instantiate(arrowPrefab, new Vector3(0, -50, 0), Quaternion.Euler(Vector3.zero));

        actualProjectil.InitialSetUps(this, isDemon);
        myShootingController = new ShootingController(actualProjectil, mouseCollisions);

        myMovementController = new MovementController(this);

        if (isDemon)
            myAnimatorController = new AnimatorDemon(myAnimator);
        else
            myAnimatorController = new AnimatorAngel(myAnimator);

        myButtonController = new ButtonsController(this);
        _lifeController = new LifeController(isDemon);

        EventManager.Subscribe("ChangePlayer", ChangeCharacter);
        EventManager.Subscribe("TPPlayers", GoToTransform);

        if (isActive)
            StartCoroutine(CorrutinaTurnOn());
        else
            TurnOff();
    }

    void Update()
    {
        myButtonController.actualAxies();
        myButtonController.actualButtons();
        myMovementController.actualMovement();

        if (Input.GetKeyDown(KeyCode.T))
            TakeDmg();
    }


    public void CheckInteractable(KeyCode keyDown)
    {
        if (_playerInteractable == null) return;

        if (keyDown == KeyCode.F)
            _playerInteractable.Inter_DoPlayerAction(this, isDemon);
        else if (keyDown == KeyCode.Space)
            _playerInteractable.Inter_DoJumpAction(this, isDemon);
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
        myCollider.isTrigger = false;
        yield return new WaitForEndOfFrame();
        myMovementController.ChangeToMove();
        myButtonController.ButtonsOn();
        realCharacter.SetActive(true);
        totemCharacter.SetActive(false);
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

    public void GoToTransform(params object[] parameter)
    {
        transform.position = (Vector3)parameter[0];
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

    public void TakeDmg()
    {
        if (!isActive) return;
        
        StopAllCoroutines();
        SoundManager.instance.PlaySound(SoundID.GRUNT_1);
        _lifeController.TakeDamage();
        StartCoroutine(HealthTimer());
    }

    IEnumerator HealthTimer()
    {
        yield return new WaitForSeconds(5f);
        bool canHealth = _lifeController.Health();

        if (canHealth)
            StartCoroutine(HealthTimer());
    }
}
