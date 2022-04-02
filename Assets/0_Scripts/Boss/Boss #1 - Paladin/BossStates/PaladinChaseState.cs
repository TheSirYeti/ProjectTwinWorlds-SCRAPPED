using UnityEngine;

public class PaladinChaseState : IState
{
    public Transform target, hunter;
    private FiniteStateMachine _fsm;
    private Animator _animator;
    private float speed;
    private float minDistance;
    
    public void OnStart()
    {
        
    }

    public void OnUpdate()
    {
        Vector3 direction = target.transform.position - hunter.transform.position;
        direction.Normalize();
        hunter.transform.position = direction * speed * Time.deltaTime;
        if (Vector3.Distance(target.transform.position, hunter.transform.position) <= minDistance)
        {
            
        }
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
