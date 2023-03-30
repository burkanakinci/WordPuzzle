using UnityEngine;
using System.Collections.Generic;

public class PlayerStateMachine
{
    private IState m_CurrentState;
    private List<IState> m_States;

    public PlayerStateMachine(Player _player)
    {
        m_States = new List<IState>();
    }

    public void LogicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.LogicalUpdate();
        }
    }
    public void PhysicalUpdate()
    {
        if (m_CurrentState != null)
        {
            m_CurrentState.PhysicalUpdate();
        }
    }
    public void ChangeState(PlayerStates _state, bool _changeForce = false)
    {
        if (m_States[(int)_state] != m_CurrentState || _changeForce)
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Exit();
            }

            m_CurrentState = m_States[(int)_state];
            m_CurrentState.Enter();
        }
    }

    public bool EqualCurrentState(int _state)
    {
        return (m_CurrentState == m_States[_state]);
    }

    public IState GetState(PlayerStates _state)
    {
        return m_States[(int)_state];
    }
}
