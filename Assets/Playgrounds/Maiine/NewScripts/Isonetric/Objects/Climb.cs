using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : BaseInteractable, IWeaponInteractable
{
    delegate void ClimbDelegate();
    ClimbDelegate actualMove = delegate { };

    bool _isConnect = false;

    bool canRight;
    bool canLeft;

    Vector3 _pointToGo;
    public float _speed;

    public float initialSpeed;
    public float minDist;

    public LayerMask wallMaks;
    public LayerMask blockMask;
    public LineRenderer lineRenderer;
    public GameObject myWall = null;
    public Transform grapPoint;

    void Update()
    {
        actualMove();
    }

    #region Interiores del delegate
    void Delegate_LineRendererWork()
    {
        lineRenderer.SetPosition(0, grapPoint.position);
        lineRenderer.SetPosition(1, _actualPlayer.transform.position);
    }

    void Delegate_GoToPlayer()
    {
        if (!canLeft || !canRight)
        {
            if (Vector3.Distance(grapPoint.position, _pointToGo) > minDist)
            {
                _actualBullet.Bullet_Reset();
                grapPoint.localPosition = Vector3.zero;
            }
        }

        grapPoint.position += (_pointToGo - grapPoint.position) * initialSpeed * Time.deltaTime;

        if (Vector3.Distance(grapPoint.position, _pointToGo) < minDist)
        {
            actualMove -= Delegate_GoToPlayer;
            _actualPlayer.myMovementController.ChangeToClimb(this, grapPoint);
            _actualPlayer.transform.parent = grapPoint;
            _actualPlayer.myButtonController.ChangeAxies(true);
        }
    }
    #endregion

    #region Publicas llamadas desde el Player
    public void MoveGrapPoint(float rightMovement)
    {
        rightMovement = RayCastGrapCheck(rightMovement);

        if (!canRight && rightMovement > 0)
            rightMovement = 0;

        if (!canLeft && rightMovement < 0)
            rightMovement = 0;

        Vector3 grapMovement = transform.right * rightMovement;

        grapPoint.position += grapMovement * _speed * Time.deltaTime;
    }

    float RayCastGrapCheck(float movementX)
    {
        if (Physics.Raycast(_actualPlayer.transform.position, transform.right * movementX, 0.5f, blockMask))
        {
            return 0f;
        }
        else
        {
            return movementX;
        }
    }

    public void SetMovement(bool side, bool state)
    {
        if (side)
            canRight = state;
        else
            canLeft = state;
    }
    #endregion

    #region Interfaces
    public void Inter_DoWeaponAction(BulletSystem bullet)
    {
        _actualBullet = bullet;

        _isOnUse = true;
        lineRenderer.enabled = true;
        _pointToGo = new Vector3(_actualPlayer.transform.position.x, grapPoint.position.y, _actualPlayer.transform.position.z);
        actualMove += Delegate_LineRendererWork;
        actualMove += Delegate_GoToPlayer;
        _actualPlayer.myMovementController.ChangeToStay();
    }

    public void Inter_DoConnectAction(IWeaponInteractable otherObject)
    {

    }

    public void Inter_ResetObject()
    {
        _isOnUse = false;
        lineRenderer.enabled = false;
        _actualPlayer.myMovementController.ChangeToMove();
        _actualPlayer.myButtonController.ChangeAxies(false);
        _actualPlayer.myMovementController.SetUp(true);
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

        //Checkeo que te pegado a la pared
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1, wallMaks);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") && myWall == collider.gameObject
                && _isUsableByDemon == isDemon)
            {
                _actualPlayer = actualPlayer;
                return true;
            }
        }

        return false;
    }

    public bool Inter_OnUse()
    {
        return _isOnUse;
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = grapPoint;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }

    public GameObject Inter_GetGameObject()
    {
        return this.gameObject;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && myWall == null)
        {
            myWall = other.gameObject;
        }
    }

}
