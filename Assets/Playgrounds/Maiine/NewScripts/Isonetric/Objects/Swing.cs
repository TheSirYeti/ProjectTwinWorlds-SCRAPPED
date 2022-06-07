using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : BaseInteractable, IWeaponInteractable
{
    public LayerMask wallMaks;

    float _horizontal;

    public bool canSwing = false;

    public float maxAngleDeflection = 0f;
    public float maxAngle = 90f;
    public float upgradeSpeed;
    public float decreaseSpeed;
    public float speedOfPendulum = 1.0f;

    public Transform rotationPoint;
    public Transform playerPosition;

    public Transform actualLook;
    public Transform rigthLook, leftLook;

    public LayerMask collisionFloor;

    public int actualDir = 0;

    bool isPlayer = true;
    IWeaponInteractable connectedObject;


    delegate void SwingDelegate();
    SwingDelegate actualMove = delegate { };

    void Update()
    {
        actualMove();
    }

    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _isOnUse = true;
        isPlayer = true;
        _actualBullet = bullet;
        _actualPlayer.transform.parent = rotationPoint;
        _actualPlayer.transform.position = playerPosition.position;
        _actualPlayer.myMovementController.ChangeToSwing(this);
        _actualPlayer.myButtonController.ChangeAxies(true);
        _actualPlayer.isUsingGravity = false;
        actualMove = Delegate_Swing;
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {
        _isOnUse = true;
        isPlayer = false;
        connectedObject = otherObject;
        otherObject.Inter_GetGameObject().transform.parent = rotationPoint;
        otherObject.Inter_GetGameObject().transform.position = playerPosition.position;
        otherObject.Inter_GetGameObject().GetComponent<Rigidbody>().Sleep();
        _actualPlayer.myMovementController.ChangeToSwing(this);
        _actualPlayer.myButtonController.ChangeAxies(true);
        actualMove = Delegate_Swing;
    }

    public void Inter_ResetObject()
    {
        _isOnUse = false;

        _actualPlayer.myMovementController.ChangeToMove();
        _actualPlayer.myButtonController.ChangeAxies(false);


        if (isPlayer)
        {
            _actualPlayer.transform.parent = null;
            _actualPlayer.isUsingGravity = true;
        }
        else
        {
            connectedObject.Inter_GetGameObject().transform.parent = null;
            connectedObject.Inter_GetGameObject().GetComponent<Rigidbody>().WakeUp();
        }


        if (actualDir == 1)
        {
            if (isPlayer)
                _actualPlayer.myMovementController.SetForce(rigthLook.position - playerPosition.position, (maxAngleDeflection / maxAngle));
            else
                connectedObject.Inter_GetGameObject().GetComponent<MovableBox>().SetForce(rigthLook.position - playerPosition.position, (maxAngleDeflection / maxAngle));
        }
        else if (actualDir == 2)
        {
            if (isPlayer)
                _actualPlayer.myMovementController.SetForce(leftLook.position - playerPosition.position, (maxAngleDeflection / maxAngle));
            else
                connectedObject.Inter_GetGameObject().GetComponent<MovableBox>().SetForce(leftLook.position - playerPosition.position, (maxAngleDeflection / maxAngle));
        }


        maxAngleDeflection = 0;
        rotationPoint.localRotation = Quaternion.Euler(0, 0, 0);
        actualDir = 0;
        actualMove = delegate { };
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        //Checkeo si esta en Spot
        if (!canSwing && !_isOnUse) return false;

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
    }

    public GameObject Inter_GetGameObject()
    {
        return this.gameObject;
    }


    public void SetDir(float dir)
    {
        _horizontal = dir;
    }


    void Delegate_Swing()
    {
        float angle = maxAngleDeflection * Mathf.Sin(Time.time * speedOfPendulum);
        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle);


        if (maxAngleDeflection == 0)
            actualDir = 0;
        else if (maxAngleDeflection - Mathf.Abs(angle) < maxAngleDeflection * 0.02f)
        {
            if (angle > 0)
            {
                actualDir = 2;
                actualLook = leftLook;
            }
            else if (angle < 0)
            {
                actualDir = 1;
                actualLook = rigthLook;
            }
        }


        CheckInitialDir();


        if (actualDir == 1)
        {
            if (_horizontal > 0)
                maxAngleDeflection += upgradeSpeed * Time.deltaTime;
            else if (_horizontal < 0)
                maxAngleDeflection -= upgradeSpeed * 0.8f * Time.deltaTime;
        }
        else if (actualDir == 2)
        {
            if (_horizontal < 0)
                maxAngleDeflection += upgradeSpeed * Time.deltaTime;
            else if (_horizontal > 0)
                maxAngleDeflection -= upgradeSpeed * 0.8f * Time.deltaTime;
        }


        if (_horizontal == 0)
        {
            maxAngleDeflection -= decreaseSpeed * Time.deltaTime;
        }



        maxAngleDeflection = Mathf.Clamp(maxAngleDeflection, 0, maxAngle);
    }

    void CheckInitialDir()
    {
        if (actualDir == 0)
        {
            if (_horizontal > 0)
            {
                actualDir = 1;
                maxAngleDeflection = 15;
                actualLook = rigthLook;
            }
            else if (_horizontal < 0)
            {
                actualDir = 2;
                maxAngleDeflection = 15;
                actualLook = leftLook;
            }
        }
    }
}
