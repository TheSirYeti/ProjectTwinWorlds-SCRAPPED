using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    IState _currentState = new NullState();
    Dictionary<PaladinState, IState> _allStates = new Dictionary<PaladinState, IState>();


    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void AddState(PaladinState id, IState state)
    {
        if (_allStates.ContainsKey(id)) return;

        _allStates.Add(id, state);
    }

    public void ChangeState(PaladinState id)
    {
        if (!_allStates.ContainsKey(id)) return;
        _currentState.OnExit();
        _currentState = _allStates[id]; 
        _currentState.OnStart();
    }
}

public enum PaladinState
{
    REST,
    CHASE,
    BREACH,
    SUMMON,
    TACKLE,
    RETURN
}
