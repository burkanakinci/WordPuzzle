using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudArea : BaseArea<HudPanel>
{
    [Header("Level Values")]
    [SerializeField] private TextMeshProUGUI m_LevelNumberText;
    [SerializeField] private TextMeshProUGUI m_LevelTitleText;


    [Header("Hud Buttons")]
    [SerializeField] private CheckWordButton m_CheckWordButton;

    [Header("Matched Text")]
    [SerializeField] private TextMeshProUGUI m_MatchedText;

    public override void Initialize(HudPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);

        CachedComponent.OnLevelStartPanelEvent += SetLevelNumberText;
        CachedComponent.OnLevelStartPanelEvent += SetLevelTitleText;
        CachedComponent.OnLevelStartPanelEvent += ResetMatchedText;
        GameManager.Instance.LevelManager.WordManager.OnSubmitWord += OnSubmitWord;

        m_CheckWordButton.Initialize(this);
    }



    #region Events
    private void SetLevelNumberText()
    {
        m_LevelNumberText.text = "Level : " + GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber;
    }
    private void SetLevelTitleText()
    {
        m_LevelTitleText.text = GameManager.Instance.LevelManager.CurrentLevelData.title;
    }
    private void ResetMatchedText()
    {
        m_MatchedText.text = "";
    }
    private void OnSubmitWord(bool _isCorrect, string _word)
    {
        if (_isCorrect)
        {
            m_MatchedText.text += _word;
            m_MatchedText.text += "\n";
        }
    }
    #endregion
}
