
using System.Collections.Generic;
public class Player : CustomBehaviour<PlayerManager>
{
    private PlayerData m_PlayerData;
    public PlayerData PlayerData => m_PlayerData;
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public override void Initialize(PlayerManager _playerManager)
    {
        base.Initialize(_playerManager);
        GameManager.Instance.JsonConverter.LoadPlayerData(ref m_PlayerData);

        PlayerStateMachine = new PlayerStateMachine(this);
    }

    public void SetLevel(int _level)
    {
        m_PlayerData.PlayerLevel = _level;
        GameManager.Instance.JsonConverter.SetPlayerData(m_PlayerData);
    }

    public void AddHighscore(int _level, int _highscore)
    {
        if (m_PlayerData.HighScores.ContainsKey(_level))
        {
            m_PlayerData.HighScores[_level] = _highscore;
        }
        else
        {
            m_PlayerData.HighScores.Add(_level, _highscore);
        }
        GameManager.Instance.JsonConverter.SetPlayerData(m_PlayerData);
    }
}
