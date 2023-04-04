using UnityEngine;
using System;
public class PlayerManager : CustomBehaviour
{
    public event Action<int> OnAddedHighScoreEvent;
    #region Fields
    [SerializeField] private Player m_Player;
    #endregion
    public override void Initialize()
    {
        base.Initialize();
        m_Player.Initialize(this);

        GameManager.Instance.OnMainMenuEvent += OnMainMenu;
        GameManager.Instance.OnLevelStartEvent += OnGameStart;
        GameManager.Instance.OnSuccessEvent += OnSuccess;
        GameManager.Instance.OnFailedEvent += OnFailed;
    }

    #region PlayerDataAccess
    #region Setters
    public void SetPlayerLevel(int _level)
    {
        m_Player.SetLevel(_level);
    }
    public void AddPlayerNewHighscore(int _level, int _highscore)
    {
        m_Player.AddHighscore(_level, _highscore);
        OnAddedHighScoreEvent?.Invoke(_highscore);
    }
    #endregion

    #region Getters
    public int GetPlayerLevel()
    {
        return m_Player.PlayerData.PlayerLevel;
    }
    public bool HasPlayerHighscore(int _levelNumber)
    {
        return m_Player.PlayerData.HighScores.ContainsKey(_levelNumber);
    }
    public int GetPlayerHighscore(int _levelNumber)
    {
        return m_Player.PlayerData.HighScores[_levelNumber];
    }
    #endregion
    #endregion

    #region Events
    private void OnMainMenu()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.MainState, true);
    }
    private void OnGameStart()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.GameplayState);
    }
    private void OnSuccess()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.SuccessState);
    }
    private void OnFailed()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.FailedState);
    }
    private void OnIncreaseScore(int _score)
    {
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnMainMenuEvent -= OnMainMenu;
        GameManager.Instance.OnLevelStartEvent -= OnGameStart;
        GameManager.Instance.OnSuccessEvent -= OnSuccess;
        GameManager.Instance.OnFailedEvent -= OnFailed;
    }
    #endregion
}
