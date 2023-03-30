using System.Collections.Generic;
using UnityEngine;

public class UIManager : CustomBehaviour
{
    #region Fields
    private UIPanel m_CurrentUIPanel;
    [SerializeField] private List<UIPanel> m_UIPanels;
    #endregion
    public override void Initialize(GameManager _gameManager)
    {
        base.Initialize(_gameManager);

        m_UIPanels.ForEach(_panel =>
        {
            _panel.Initialize(this);
            _panel.gameObject.SetActive(true);
        });

        GameManager.OnMainMenuEvent += OnMainMenu;
        GameManager.OnGameStartEvent += OnGameStart;
        GameManager.OnSuccessEvent += OnSuccess;
        GameManager.OnFailedEvent += OnFailed;
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
        m_CurrentUIPanel = _panel;
    }
    public void SetCurrentUIPanel(UIPanelType _panel)
    {
        m_CurrentUIPanel = m_UIPanels[(int)_panel];
    }
    #endregion

    #region Events
    private void OnMainMenu()
    {
        GetPanel(UIPanelType.MainMenuPanel).ShowPanel();
    }
    private void OnGameStart()
    {
        GetPanel(UIPanelType.HudPanel).ShowPanel();
    }
    private void OnSuccess()
    {
        GetPanel(UIPanelType.OnSuccess).ShowPanel();
    }
    private void OnFailed()
    {
        GetPanel(UIPanelType.OnFailed).ShowPanel();
    }
    private void OnDestroy()
    {
        GameManager.OnMainMenuEvent -= OnMainMenu;
        GameManager.OnGameStartEvent -= OnGameStart;
        GameManager.OnSuccessEvent -= OnSuccess;
        GameManager.OnFailedEvent -= OnFailed;
    }
    #endregion

}
