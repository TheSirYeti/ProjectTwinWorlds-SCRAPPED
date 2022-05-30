using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour, IWeaponInteractable
{
    delegate void ClimbDelegate();
    ClimbDelegate actualMove = delegate { };

    public bool _usableByDemon;
    bool _isConnect = false;

    bool canRight;
    bool canLeft;
    Vector3 _pointToGo;
    public float _speed;

    public float initialSpeed;
    public float minDist;

    Player _actualPlayer;

    public LayerMask layerMaks;
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
        grapPoint.position += (_pointToGo - grapPoint.position) * initialSpeed * Time.deltaTime;

        if (Vector3.Distance(grapPoint.position, _pointToGo) < minDist)
        {
            actualMove -= Delegate_GoToPlayer;
            _actualPlayer.myMovementController.ChangeToClimb(this, grapPoint);
            _actualPlayer.transform.parent = grapPoint;
            _actualPlayer.myButtonController.ChangeAxies();
        }
    }
    #endregion

    #region Publicas llamadas desde el Player
    public void MoveGrapPoint(Vector3 dir)
    {
        if (!canRight && dir.x > 0)
            dir.x = 0;

        if (!canLeft && dir.x < 0)
            dir.x = 0;


        grapPoint.position += dir * _speed * Time.deltaTime;
    }

    public void SetMovement(bool side, bool state)
    {
        if (side)
            canRight = state;
        else
            canLeft = state;
    }
    #endregion

    public void Inter_DoWeaponAction()
    {
        _isConnect = true;
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
        _isConnect = false;
        lineRenderer.enabled = false;
        _actualPlayer.myMovementController.ChangeToMove();
        _actualPlayer.myButtonController.ChangeAxies();
        _actualPlayer.transform.parent = null;
        actualMove = delegate { };
    }

    public bool Inter_CheckCanUse(Player actualPlayer, bool isDemon)
    {
        _actualPlayer = actualPlayer;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1, layerMaks);

        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall") && myWall == collider.gameObject && _usableByDemon == isDemon)
            {
                return true;
            }
        }

        return false;
    }

    public bool Inter_OnUse()
    {
        Debug.Log(_isConnect);
        return _isConnect;
    }

    public void Inter_SetParent(Transform weapon)
    {
        weapon.parent = grapPoint;
        weapon.localScale = new Vector3(1, 1, 1);
        weapon.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && myWall == null)
        {
            myWall = other.gameObject;
        }
    }

}
