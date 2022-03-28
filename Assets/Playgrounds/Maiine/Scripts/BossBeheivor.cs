using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeheivor : MonoBehaviour
{
    [SerializeField] private RotationShields _rotation;
    [SerializeField] private GameObject _demonPlayer;
    [SerializeField] private GameObject _angelPlayer;
    private GameObject _player;

    private int _life = 3;

    [SerializeField] private Material _mDmg;
    [SerializeField] private Material _mBase;

    [SerializeField] private float _speed;
    [SerializeField] private float _chargeSpeed;
    [SerializeField] private float _timerCap;
    private float _timer;
    private Vector3 _playerPos;

    public delegate void _BossDelegate();
    public _BossDelegate _move;

    private Vector3 _dir;

    [SerializeField] private LayerMask _pentadente;

    private Renderer _renderer;

    private void Start()
    {
        _move = Movement;
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (_demonPlayer.activeSelf)
        {
            _player = _demonPlayer;
        }
        else
        {
            _player = _angelPlayer;
        }

        _move();

        if (_timer > _timerCap)
        {
            StartCoroutine(PrepareCharge());
        }
    }

    void Movement()
    {
        _dir = _player.transform.position - transform.position;
        transform.forward = _dir;
        transform.position += transform.forward * _speed * Time.deltaTime;
        _timer += Time.deltaTime;
    }

    void SetCharge()
    {
        _playerPos = _player.transform.position;
        _dir = _playerPos - transform.position;
        transform.forward = _dir;
        _timer = 0;
        _move = Charge;
    }

    void Charge()
    {
        transform.position += transform.forward * _chargeSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, _playerPos) < 0.5f)
        {
            _move = Movement;
        }
    }

    void LookAt()
    {
        _dir = _playerPos - transform.position;
        transform.forward = _dir;
    }

    IEnumerator PrepareCharge()
    {
        _move = LookAt;
        yield return new WaitForSeconds(1.5f);
        SetCharge();
    }

    IEnumerator ChangeMat()
    {
        _renderer.material = _mDmg;
        yield return new WaitForSeconds(0.5f);
        _renderer.material = _mBase;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _pentadente)
        {
            Debug.Log("dmg");
            _life--;
            StartCoroutine(ChangeMat());
        }
    }
}
