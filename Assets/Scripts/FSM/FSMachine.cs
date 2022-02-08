using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moore state machine
/// </summary>
public class FSMachine
{
    FSMState _currentState;

    public FSMachine(FSMState startingState)
    {
        _currentState = startingState;
    }

    /// <summary>
    /// Update and execute current state.
    /// </summary>
    public void Update()
    {
        FSMState nextState;

        if (_currentState.TryTransitionToChildren(out nextState))
        {
            SetState(nextState);
        }

        _currentState.Execute();
    }

    void SetState(FSMState state)
    {
        _currentState = state;
    }

}
