using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    IState _currentState = new NullState();
    Dictionary<FSM_State, IState> _allStates = new Dictionary<FSM_State, IState>();


    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void AddState(FSM_State id, IState state)
    {
        if (_allStates.ContainsKey(id)) return;

        _allStates.Add(id, state);
    }

    public void ChangeState(FSM_State id)
    {
        if (!_allStates.ContainsKey(id)) return;
        _currentState.OnExit();
        _currentState = _allStates[id]; 
        _currentState.OnStart();
    }
}

public enum FSM_State
{
    PALADIN_REST,
    PALADIN_CHASE,
    PALADIN_BREACH,
    PALADIN_SUMMON,
    PALADIN_TACKLE,
    PALADIN_RETURN,
    PALADIN_DEATH,
    
    MAFIA_RELOAD,
    MAFIA_SHOOT
}
