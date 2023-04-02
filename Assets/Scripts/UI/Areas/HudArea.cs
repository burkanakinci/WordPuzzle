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

    public override void Initialize(HudPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);

        CachedComponent.OnLevelStartPanelEvent += SetLevelNumberText;
        CachedComponent.OnLevelStartPanelEvent += SetLevelTitleText;
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
    #endregion
}
