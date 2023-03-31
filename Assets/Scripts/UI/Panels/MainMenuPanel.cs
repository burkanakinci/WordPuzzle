using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuPanel : UIPanel
{
    [SerializeField] protected List<BaseArea<MainMenuPanel>> m_MainManuAreas;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        m_MainManuAreas.ForEach(_area =>
        {
            _area.Initialize(this);
        });
    }

    public override void HideAllArea()
    {
        m_MainManuAreas.ForEach(_area =>
        {
            _area.HideArea();
        });
    }
    public override void ShowArea(int _area)
    {
        m_MainManuAreas[_area].ShowArea();
    }
}


