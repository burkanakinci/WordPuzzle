using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuccessPanel : UIPanel
{
    [SerializeField] private UIBaseButton m_OpenMainArea;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_OpenMainArea.Initialize(_uiManager.GameManager);
        m_OpenMainArea.ButtonClickAction += (() => _uiManager.GameManager.OnMainMenu());
    }

}


