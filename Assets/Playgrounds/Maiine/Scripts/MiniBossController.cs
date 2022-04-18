using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runningSpeed;
    [SerializeField] private LayerMask layerMaskC;
    [SerializeField] private LayerMask layerMaskW;
    [SerializeField] private Animator _ani;
    private Vector3 _goTo;
    private float timer;

    delegate void Movement();
    Movement movement;
    
    void Start()
    {
        movement = Following;
        _player = PlayerWorlds.instance.currentPlayer;
    }

    void Update()
    {
        movement();
    }

    //Cambio de delegate desde animator
    public void StartFollowing()
    {
        movement = Following;
    }

    public void StartRunning()
    {
        SetObjective();
        transform.LookAt(new Vector3(_goTo.x, transform.position.y, _goTo.z));
        movement = Running;
    }

    public void Stay()
    {
        movement = delegate { };
    }

    //Interior de delegates
    void Following()
    {
        transform.LookAt(new Vector3(PlayerWorlds.instance.currentPlayer.transform.position.x, transform.position.y, PlayerWorlds.instance.currentPlayer.transform.position.z));
        transform.position += transform.forward * Time.deltaTime * _walkSpeed;
        timer += Time.deltaTime;
        if(timer > 2f)
        {
            timer = 0;
            _ani.SetTrigger("StartPreAtack");
        }
    }

    void Running()
    {
        transform.position += transform.forward * Time.deltaTime * _runningSpeed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, layerMaskC))
        {
            _ani.SetTrigger("HitWall");
            hit.collider.gameObject.GetComponent<ColumnasRompibles>().InitialBreak();
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, layerMaskW))
        {
            _ani.SetTrigger("HitWall");
        }
    }

    //Set nuevo objetivo desde animator
    void SetObjective()
    {
        _goTo = PlayerWorlds.instance.currentPlayer.transform.position;
    }
}
