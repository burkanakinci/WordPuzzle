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
        GameManager.Instance.UIManager.GetPanel(UIPanelType.MainMenuPanel).ShowPanel();
        GameManager.Instance.UIManager.GetPanel(UIPanelType.MainMenuPanel).HideAllArea();
        GameManager.Instance.UIManager.GetPanel(UIPanelType.MainMenuPanel).ShowArea((int)MainMenuAreas.MainArea);
    }
    public void Exit()
    {

    }
}
