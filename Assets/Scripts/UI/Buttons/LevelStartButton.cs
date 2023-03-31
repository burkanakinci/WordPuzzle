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
        AddFunctionToButtonListener(() => GameManager.Instance.LevelManager.StartLevel(CachedComponent.PopupLevelData));
    }

    public void DisableLevelStartButton()
    {
        m_ButtonBG.color = Color.grey;
        m_Button.enabled = false;
    }

    public void EnableLevelStartButton()
    {
        m_ButtonBG.color = Color.green;
        m_Button.enabled = true;
    }
}
