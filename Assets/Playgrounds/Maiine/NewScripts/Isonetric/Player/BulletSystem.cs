using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    delegate void BulletMovement();
    BulletMovement actualMovement = delegate { };

    public LineRenderer lineRenderer;
    Transform actualLineTransform; 

    Player _myPlayer;
    Transform _handPoint;
    bool _isDemon;
    bool _isOnPlayer = true;
    Vector3 _pointToGo;
    IWeaponInteractable _actualCollider = null;
    IWeaponInteractable _connectedCollider = null;

    [SerializeField]
    float _speed;
    
    [SerializeField]
    float _backSpeed;

    [SerializeField]
    float _distanceToInteract;

    [SerializeField]
    float _radius;

    [SerializeField]
    LayerMask _interactableItems;

    public void InitialSetUps(Player myPlayer, bool isDemon)
    {
        _myPlayer = myPlayer;
        _isDemon = isDemon;
        _handPoint = myPlayer.handPoint;
        transform.parent = _handPoint;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        actualLineTransform = _handPoint.transform;
    }

    void Update()
    {
        actualMovement();

        LineRenderer();
    }

    void LineRenderer()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, actualLineTransform.position);
    }

    void Delegate_ReturnPlayer()
    {
        Vector3 dir = (_myPlayer.transform.position - transform.position).normalized;

        transform.forward = dir;

        transform.position += transform.forward * _backSpeed * Time.deltaTime;

        if (Vector3.Distance(_myPlayer.transform.position, transform.position) < _distanceToInteract)
        {
            _isOnPlayer = true;
            actualLineTransform = _handPoint.transform;
            transform.parent = _myPlayer.handPoint;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            actualMovement = delegate { };
        }
    }

    void Delegate_MoveTo()
    {
        Vector3 dir = (_pointToGo - transform.position).normalized;
        transform.position += dir * _speed * Time.deltaTime;

        if (Vector3.Distance(_pointToGo, transform.position) < _distanceToInteract)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius, _interactableItems);

            if (hitColliders.Length > 0)
            {
                _actualCollider = hitColliders[0].GetComponent<IWeaponInteractable>();
                _actualCollider.Inter_SetParent(transform);
            }

            actualMovement = delegate { };
        }
    }

    bool Bool_IsOnPlayer()
    {
        return _isOnPlayer;
    }

    bool Bool_IsOnObject()
    {
        if (_actualCollider != null)
            return true;
        else
            return false;
    }

    public void Bullet_Shoot(Vector3 objectToGo, IWeaponInteractable objectAimed)
    {
        if (!Bool_IsOnPlayer())
        {
            Bullet_Interactive(objectAimed);
            return;
        }
        _isOnPlayer = false;
        transform.parent = null;
        _pointToGo = objectToGo;

        transform.forward = _pointToGo - transform.position;

        actualMovement = Delegate_MoveTo;
    }

    public void Bullet_Interactive(IWeaponInteractable objectAimed)
    {
        if (objectAimed == null || !Bool_IsOnObject())
        {
            Bullet_Reset();
            return;
        }

        if (objectAimed.Inter_CheckCanUse(_myPlayer, _isDemon) && !_actualCollider.Inter_OnUse())
        {
            SoundManager.instance.PlaySound(SoundID.THROW_HIT);
            if (_actualCollider == objectAimed)
            {
                objectAimed.Inter_DoWeaponAction(this);
            }
            else
            {
                _connectedCollider = objectAimed;
                actualLineTransform = _connectedCollider.Inter_GetGameObject().transform;
                objectAimed.Inter_DoConnectAction(_actualCollider);
            }
        }
        else
        {
            Bullet_Reset();
        }
    }

    public void Bullet_Reset()
    {
        if (_actualCollider != null && _actualCollider.Inter_CheckCanUse(_myPlayer, _isDemon))
        {
            _actualCollider.Inter_ResetObject();
            _actualCollider = null;
        }

        if (_connectedCollider != null && _connectedCollider.Inter_CheckCanUse(_myPlayer, _isDemon))
        {
            _connectedCollider.Inter_ResetObject();
            _connectedCollider = null;
        }

        actualMovement = Delegate_ReturnPlayer;
    }
}
