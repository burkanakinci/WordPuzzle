using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuPanel : UIPanel
{
    [SerializeField] private List<BaseArea<MainMenuPanel>> m_MainMenuAreas;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_MainMenuAreas.ForEach(_area =>
        {
            _area.Initialize(this);
        });
    }
}


