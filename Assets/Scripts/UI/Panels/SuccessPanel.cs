using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuccessPanel : UIPanel
{
    [SerializeField] private UIBaseButton<SuccessPanel> m_OpenLevelArea;
    [SerializeField] private HighScoreArea m_HighScoreArea;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        OnSuccessLevelEvent += () =>
        {
            m_HighScoreArea.ShowArea();
        };
        m_OpenLevelArea.Initialize(this);
        m_OpenLevelArea.AddFunctionToButtonListener(() => GameManager.Instance.OnMainMenu());
    }

}


