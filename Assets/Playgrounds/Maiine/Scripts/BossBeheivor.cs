using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeheivor : MonoBehaviour
{
    [SerializeField] private List<GameObject> _shields;
    [SerializeField] private List<GameObject> _shieldsFaseTwo;

    [SerializeField] private GameObject _secondFase;
    [SerializeField] private GameObject _thirdFase;

    private int _actualFase = 1;

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
        transform.LookAt(_player.transform);
        transform.position += transform.forward * _speed * Time.deltaTime;
        _timer += Time.deltaTime;
    }

    void SetCharge()
    {
        _playerPos = _player.transform.position;
        _timer = 0;
        _move = Charge;
    }

    void Charge()
    {
        transform.position += transform.forward * _chargeSpeed * Time.deltaTime;
        _timer += Time.deltaTime;
        if(_timer > 2)
        {
            _timer = 0;
            _move = Movement;
        }
    }

    void Follow()
    {
        transform.LookAt(_player.transform);
    }

    IEnumerator PrepareCharge()
    {
        _move = Follow;
        yield return new WaitForSeconds(1.5f);
        SetCharge();
    }

    IEnumerator ChangeMat()
    {
        _renderer.material = _mDmg;
        yield return new WaitForSeconds(0.5f);
        _renderer.material = _mBase;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("trigger");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Pentadente"))
        {
            Debug.Log("dmg");
            _life--;
            StartCoroutine(ChangeMat());
            _actualFase++;
            if(_actualFase == 2)
            {
                _secondFase.gameObject.SetActive(true);
                foreach (var item in _shields)
                {
                    item.gameObject.SetActive(true);
                }
            }
            else if (_actualFase == 3)
            {
                _thirdFase.gameObject.SetActive(true);
                foreach (var item in _shieldsFaseTwo)
                {
                    item.gameObject.SetActive(true);
                }
            }
            else if(_actualFase == 4)
            {
                Destroy(gameObject);
            }
        }
    }
}
