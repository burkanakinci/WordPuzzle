using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FailedPanel : UIPanel
{
    [SerializeField] private UIBaseButton<FailedPanel> m_OpenMainArea;
    public override void Initialize(UIManager _uiManager )
    {
        base.Initialize(_uiManager);
        m_OpenMainArea.Initialize(this);
        m_OpenMainArea.AddFunctionToButtonListener((() => GameManager.Instance.OnMainMenu()));
    }
}


