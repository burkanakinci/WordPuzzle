using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPopup : CustomBehaviour<LevelPopupArea>
{
    [SerializeField] private TextMeshProUGUI m_LevelNameText;
    [SerializeField] private TextMeshProUGUI m_LevelTitleText;
    [SerializeField] private TextMeshProUGUI m_HighScoreText;
    [SerializeField] private LevelStartButton m_LevelStartButton;
    public override void Initialize(LevelPopupArea _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }

    public LevelData PopupLevelData { get; private set; }
    private int m_LevelCount;
    public void SetLevelPopupData(int _level, LevelData _levelData)
    {
        PopupLevelData = _levelData;
        PopupLevelData.LevelNumber = _level;
        m_LevelCount = _level;
        m_LevelStartButton.Initialize(this);
        
        SetLevelPopup();
    }

    private void SetLevelPopup()
    {
        SetLevelPopupTexts();
        SetLevelStartButton();
    }

    private void SetLevelPopupTexts()
    {
        m_LevelNameText.text = "Level : " + m_LevelCount;
        m_LevelTitleText.text = PopupLevelData.title;

        if (GameManager.Instance.PlayerManager.HasPlayerHighscore(PopupLevelData.LevelNumber))
        {
            m_HighScoreText.text = "Highscore : " + GameManager.Instance.PlayerManager.GetPlayerHighscore(PopupLevelData.LevelNumber);
        }
        else
        {
            m_HighScoreText.text = "";
        }
    }
    private void SetLevelStartButton()
    {
        if (GameManager.Instance.PlayerManager.GetPlayerLevel() >= PopupLevelData.LevelNumber)
        {
            m_LevelStartButton.EnableLevelStartButton();
        }
        else
        {
            m_LevelStartButton.DisableLevelStartButton();
        }
    }
}
