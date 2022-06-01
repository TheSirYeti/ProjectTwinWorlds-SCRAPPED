using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    protected Player _actualPlayer;
    protected BulletSystem _actualBullet;
    protected bool _isOnUse = false;

    [SerializeField]
    protected LayerMask _ignoreInteractableMask;

    [SerializeField]
    protected float _distanceToInteract;

    [SerializeField]
    protected bool _isUsableByDemon;
}
