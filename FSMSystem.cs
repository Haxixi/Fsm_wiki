using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DLFramework;

public class FSMSystem
{

    public List<FSMState> States;
    public bool isTransition;
    private StateID _NextStateID;
    public StateID NextStateId
    {
        get { return _NextStateID; }
    }

    private FSMState _CurrentState;
    public FSMState CurrentState
    {
        get { return _CurrentState; }
    }

    public FSMSystem()
    {
        States = new List<FSMState>();
    }

    public void AddState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
        }

        foreach (FSMState state in States)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                             " because state has already been added");
                return;
            }
        }

        States.Add(s);

        if (States.Count == 1)
        {
            _CurrentState = s;
            _NextStateID = StateID.NullStateID;
            return;
        }
    }

    public void PerformTransition(Transition trans)
    {

        if (isTransition)
        {
            return;
        }

        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        StateID id = _CurrentState.GetOutputState(trans);

        if (id == StateID.NullStateID)
        {
            return;
        }

        _NextStateID = id;

        foreach (FSMState state in States)
        {
            if (state.ID == _NextStateID)
            {
                _CurrentState.DoBeforeLeaving();
                state.DoBeforeEnter();
                isTransition = true;
                CoroutineTaskManager.Instance.WaitSecondTodo(() =>
                {
                    _CurrentState = state;
                    isTransition = false;
                }, _CurrentState.dic[trans]);
                break;
            }
        }
    }

}
