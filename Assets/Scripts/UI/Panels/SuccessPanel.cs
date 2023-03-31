using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuccessPanel : UIPanel
{
    [SerializeField] private UIBaseButton<SuccessPanel> m_OpenMainArea;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_OpenMainArea.Initialize(this);
        m_OpenMainArea.AddFunctionToButtonListener(() => GameManager.Instance.OnMainMenu());
    }

}


