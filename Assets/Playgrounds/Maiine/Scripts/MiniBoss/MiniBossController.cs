using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runningSpeed;

    [SerializeField] private LayerMask layerMaskC;
    [SerializeField] private LayerMask layerMaskW;

    [SerializeField] private Animator _ani;

    private Vector3 _goTo;
    private float timer;

    [SerializeField] private Material baseMat;
    [SerializeField] private Material dmgMat;
    [SerializeField] private SkinnedMeshRenderer rend;

    delegate void Movement();
    Movement movement;

    private int life = 3;

    void Start()
    {
        movement = Following;
    }

    void Update()
    {
        movement();
        Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), transform.forward, Color.red, 3f);
    }

    //Cambio de delegate desde animator
    public void StartFollowing()
    {
        movement = Following;
    }

    public void StartRunning()
    {
        movement = Running;
    }

    public void SetObjective()
    {
        _goTo = PlayerWorlds.instance.currentPlayer.transform.position;
        transform.LookAt(new Vector3(_goTo.x, transform.position.y, _goTo.z));
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
        if (timer > 2f)
        {
            timer = 0;
            _ani.SetTrigger("StartPreAtack");
        }
    }

    void Running()
    {
        transform.position += transform.forward * Time.deltaTime * _runningSpeed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, 3f, layerMaskC))
        {
            _ani.SetTrigger("HitWall");
            hit.collider.gameObject.GetComponent<ColumnasRompibles>().InitialBreak();
        }
        else if (Physics.Raycast(transform.position + new Vector3(0, 1.5f, 0), transform.forward, out hit, 3f, layerMaskW))
        {
            _ani.SetTrigger("HitWall");
        }
    }

    public void TakeDamage()
    {
        life--;
        Debug.Log("dmg");
        StartCoroutine(ChangeMat());
        if (life <= 0)
            Destroy(gameObject);
    }

    IEnumerator ChangeMat()
    {
        rend.material = dmgMat;
        yield return new WaitForSeconds(1f);
        rend.material = baseMat;
    }

}
