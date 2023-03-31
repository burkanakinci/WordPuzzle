using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStartButton : UIBaseButton<LevelPopup>
{
    [SerializeField] private Image m_ButtonBG;
    public override void Initialize(LevelPopup _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }

    protected override void OnClickAction()
    {
        GameManager.Instance.LevelManager.StartLevel(CachedComponent.PopupLevelData);
    }

    public override void DisableLevelStartButton()
    {
        m_ButtonBG.color = Color.grey;
        base.DisableLevelStartButton();
    }

    public override void EnableLevelStartButton()
    {
        m_ButtonBG.color = Color.green;
        base.EnableLevelStartButton();
    }
}
