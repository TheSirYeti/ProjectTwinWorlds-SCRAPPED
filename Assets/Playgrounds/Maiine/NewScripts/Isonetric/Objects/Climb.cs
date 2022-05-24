using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour, IWeaponInteractable
{
    delegate void ClimbMove();
    ClimbMove actualClimb = delegate { };

    bool weaponConnected;
    bool isActive;
    bool canRight;
    bool canLeft;
    float _myLength;
    Vector3 _pointToGo;
    public float _speed;

    public float initialSpeed;
    public float minDist;

    Player _actualPlayer;

    public LayerMask layerMaks;
    public LineRenderer lineRenderer;
    public GameObject myWall;
    public Transform grapPoint;

    void Start()
    {
        _myLength = transform.localScale.x;
    }

    void Update()
    {
        actualClimb();
    }

    void LineRendererWork()
    {
        lineRenderer.SetPosition(0, grapPoint.position);
        lineRenderer.SetPosition(1, _actualPlayer.transform.position);
    }

    void GoToPlayer()
    {
        grapPoint.position += (_pointToGo - grapPoint.position) * initialSpeed * Time.deltaTime;

        if (Vector3.Distance(grapPoint.position, _pointToGo) < minDist)
        {
            actualClimb -= GoToPlayer;
            _actualPlayer.myMovementController.ChangeToClimb(this, grapPoint);
            _actualPlayer.transform.parent = grapPoint;
            _actualPlayer.myButtonController.ChangeAxies();
        }
    }

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

    public void StartAction(Player actualPlayer, bool isDemon, Projectile weapon)
    {
        _actualPlayer = actualPlayer;
        if (isDemon)
            return;

        Vector3 dir = new Vector3(transform.position.x, actualPlayer.transform.position.y, transform.position.z).normalized;
        RaycastHit hit;
        if (Physics.Raycast(actualPlayer.transform.position, dir, out hit, 1, layerMaks))
        {
            if (myWall == hit.collider.gameObject)
            {
                lineRenderer.enabled = true;
                _pointToGo = new Vector3(_actualPlayer.transform.position.x, grapPoint.position.y, _actualPlayer.transform.position.z);
                actualClimb += LineRendererWork;
                actualClimb += GoToPlayer;
                actualPlayer.myMovementController.ChangeToStay();
            }
        }
    }

    public void ConnectAction()
    {

    }

    public void ResetAction()
    {
        lineRenderer.enabled = false;
        _actualPlayer.myMovementController.ChangeToMove();
        _actualPlayer.transform.parent = null;
        actualClimb = delegate { };
    }
}
