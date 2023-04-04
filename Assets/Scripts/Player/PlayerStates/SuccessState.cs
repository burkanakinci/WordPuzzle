using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessState : IState
{
    private Player m_Player;
    public SuccessState(Player _player)
    {
        m_Player = _player;
    }

    public void Enter()
    {
        GameManager.Instance.LevelManager.SetLevelEndScore();
        GameManager.Instance.UIManager.OnLevelEnd();
        if ((!m_Player.CachedComponent.HasPlayerHighscore(GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber)) ||
        (m_Player.CachedComponent.HasPlayerHighscore(GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber)
            && (m_Player.CachedComponent.GetPlayerHighscore(GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber) < GameManager.Instance.LevelManager.LevelScore)))
        {
            m_Player.CachedComponent.AddPlayerNewHighscore(GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber, GameManager.Instance.LevelManager.LevelScore);
        }

    }
    public void Exit()
    {
        GameManager.Instance.LevelManager.OnExitGameplay();
    }
}
