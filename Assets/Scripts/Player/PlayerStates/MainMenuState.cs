using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : IState
{
    private Player m_Player;
    public MainMenuState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        GameManager.Instance.UIManager.OnMainMenu();
        GameManager.Instance.UIManager.CurrentUIPanel.OnLevelStart();
    }
    public void Exit()
    {

    }
}
