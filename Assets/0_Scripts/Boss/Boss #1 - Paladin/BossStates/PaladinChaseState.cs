using UnityEngine;

public class PaladinChaseState : IState
{
    public Transform target;
    public PaladinLogic paladin;
    private FiniteStateMachine _fsm;
    private Animator _animator;
    private float speed;
    private float minDistance;
    
    public void OnStart()
    {
        if (paladin.attackNumber >= paladin.maxAttackNumber)
        {
            _fsm.ChangeState(PaladinState.RETURN);
        }
        
        _animator.SetBool("isWalking", true);
        _animator.Play("Paladin_Walking");
    }

    public void OnUpdate()
    {
        Vector3 direction = target.transform.position - paladin.transform.position;
        direction.Normalize();
        paladin.transform.position = direction * speed * Time.deltaTime;
        if (Vector3.Distance(target.transform.position, paladin.transform.position) <= minDistance)
        {
            ChooseAttack();
        }
    }

    public void OnExit()
    {
        _animator.SetBool("isWalking", false);
        paladin.attackNumber++;
    }

    void ChooseAttack()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                _fsm.ChangeState(PaladinState.BREACH);
                break;
            
            case 2:
                _fsm.ChangeState(PaladinState.SUMMON);
                break;
            
            case 3:
                _fsm.ChangeState(PaladinState.TACKLE);
                break;
        }
        
    }
}
