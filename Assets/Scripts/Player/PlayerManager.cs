using UnityEngine;

public class PlayerManager : CustomBehaviour
{
    #region Fields
    [SerializeField] private Player m_Player;
    #endregion
    public override void Initialize()
    {
        base.Initialize();
        m_Player.Initialize(this);
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
}
