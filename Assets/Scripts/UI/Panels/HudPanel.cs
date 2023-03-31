using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HudPanel : UIPanel
{
    [SerializeField] protected List<BaseArea<MainMenuPanel>> m_HudPanelAreas;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
    }

    public override void HideAllArea()
    {
        m_HudPanelAreas.ForEach(_area =>
        {
            _area.HideArea();
        });
    }
    public override void ShowArea(int _area)
    {
        m_HudPanelAreas[_area].ShowArea();
    }
}


