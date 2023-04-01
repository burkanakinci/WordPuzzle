using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HudPanel : UIPanel
{
    [SerializeField] protected List<BaseArea<HudPanel>> m_HudPanelAreas;
    
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_HudPanelAreas.ForEach(_area =>
        {
            _area.Initialize(this);
        });
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


