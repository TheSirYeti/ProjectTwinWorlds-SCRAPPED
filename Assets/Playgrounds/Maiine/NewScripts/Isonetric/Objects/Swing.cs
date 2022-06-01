using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : BaseInteractable, IWeaponInteractable
{
    GameObject _hangItem;

    public Vector3 backRigth;
    public Vector3 backLeft;
    Vector3 dir;

    bool isBackRigth = false;
    bool isBackLeft = false;

    public float timerFalling;

    public Transform midCollider;
    public Transform rotationPoint;
    public LineRenderer lineRenderer;

    public LayerMask wallMaks;

    delegate void SwingDelegate();
    SwingDelegate actualMove = delegate { };

    void Start()
    {

    }

    void Update()
    {
        actualMove();
    }

    void MoveSwing()
    {
        if (isBackRigth)
        {
            rotationPoint.Rotate(backRigth);
        }
        else if (isBackLeft)
        {
            rotationPoint.Rotate(backLeft);
        }
        else
        {
            rotationPoint.Rotate(dir);
        }
    }

    public void SetBackState(bool isRigth, bool state)
    {
        if (isRigth)
            isBackRigth = state;
        else
            isBackLeft = state;
    }

    public void CancelMovement()
    {
        isBackRigth = false;
        isBackLeft = false;
    }

    public void SetDir(float horizontal)
    {
        dir = transform.forward * horizontal * -1;
    }

    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _isOnUse = true;
        _actualBullet = bullet;
        _actualPlayer.transform.parent = rotationPoint;
        _actualPlayer.myMovementController.ChangeToSwing(this);
        _actualPlayer.myButtonController.ChangeAxies(true);
        actualMove = MoveSwing;
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        throw new System.NotImplementedException();
    }

    public void Inter_ResetObject()
    {
        _isOnUse = false;
        lineRenderer.enabled = false;
        _actualPlayer.myMovementController.ChangeToMove();
        _actualPlayer.myButtonController.ChangeAxies(false);
        _actualPlayer.transform.parent = null;
        actualMove = delegate { };
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        //Checkeo distancia
        if (Vector3.Distance(actualPlayer.transform.position, transform.position) > _distanceToInteract) return false;

        //Checkeo si no hay nada en medio
        Vector3 dir = actualPlayer.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir.normalized, out hit, Mathf.Infinity, _ignoreInteractableMask))
        {
            if (hit.collider.tag != "Player")
                return false;
        }

        if (_isUsableByDemon == isDemon)
        {
            _actualPlayer = actualPlayer;
            return true;
        }

        return false;
    }

    public bool Inter_OnUse()
    {
        return _isOnUse;
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = rotationPoint;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }

    public GameObject Inter_GetGameObject()
    {
        return this.gameObject;
    }
}
