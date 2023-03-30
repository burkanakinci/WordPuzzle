using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class JsonConverter : CustomBehaviour
{
    public override void Initialize(GameManager _gameManager)
    {
        base.Initialize(_gameManager);
    }

    private string m_TempJsonData;
    private void LoadPlayerData(ref PlayerData _playerData)
    {
        m_TempJsonData = PlayerPrefs.GetString(Constants.PLAYER_DATA);
        if (string.IsNullOrEmpty(m_TempJsonData))
        {

            _playerData = new PlayerData
            {
                PlayerLevel=1,
            };
        }
        else
        {
            _playerData = JsonUtility.FromJson<PlayerData>(m_TempJsonData);
        }
    }

    private PlayerData m_TempPlayerData;
    public void SetPlayerData(ref PlayerData _playerData)
    {
        m_TempPlayerData = _playerData;

        SavePrefs();
    }
    public void SavePrefs()
    {
        m_TempJsonData = JsonUtility.ToJson(m_TempPlayerData);
        PlayerPrefs.SetString((Constants.PLAYER_DATA), (m_TempJsonData));
        PlayerPrefs.Save();
    }
}
