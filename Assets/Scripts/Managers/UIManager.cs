using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomBehaviour
{
    #region Fields
    public UIPanel CurrentUIPanel { get; private set; }
    [SerializeField] private List<UIPanel> m_UIPanels;
    #endregion
    public override void Initialize()
    {
        base.Initialize();

        m_UIPanels.ForEach(_panel =>
        {
            _panel.Initialize(this);
            _panel.gameObject.SetActive(true);
        });
    }
    public void HideAllPanels()
    {
        m_UIPanels.ForEach(x =>
        {
            x.HidePanel();
        });
    }
    #region GetterSetter
    public UIPanel GetPanel(UIPanelType _panel)
    {
        return m_UIPanels[(int)_panel];
    }
    public void SetCurrentUIPanel(UIPanel _panel)
    {
        CurrentUIPanel = _panel;
    }
    public void SetCurrentUIPanel(UIPanelType _panel)
    {
        CurrentUIPanel = m_UIPanels[(int)_panel];
    }
    #endregion

    #region Events
    public void OnMainMenu()
    {
        GetPanel(UIPanelType.MainMenuPanel).ShowPanel();
        GetPanel(UIPanelType.MainMenuPanel).HideAllArea();
        GetPanel(UIPanelType.MainMenuPanel).ShowArea((int)MainMenuAreas.MainArea);
    }
    public void OnLevelStart()
    {
        GetPanel(UIPanelType.HudPanel).ShowPanel();
        CurrentUIPanel.OnLevelStart();
    }
    public void OnLevelEnd()
    {
        GetPanel(UIPanelType.SuccessPanel).ShowPanel();
        CurrentUIPanel.OnSuccessState();
    }
    #endregion
}
