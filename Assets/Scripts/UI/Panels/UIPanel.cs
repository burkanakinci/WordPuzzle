using UnityEngine;
using System.Collections.Generic;
using System;

public class UIPanel : CustomBehaviour<UIManager>
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    public CanvasGroup CanvasGroup => m_CanvasGroup;

    public event Action OnLevelStartPanelEvent;
    public event Action OnSuccessLevelEvent;

    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
    }

    public virtual void ShowPanel()
    {
        CachedComponent.HideAllPanels();

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }

        CanvasGroup.Open();
        SetCurrentPanel();
    }

    public virtual void HidePanel()
    {
        CanvasGroup.Close();
    }

    public virtual void SetCurrentPanel()
    {
        CachedComponent.SetCurrentUIPanel(this);
    }
    public virtual void HideAllArea()
    {
    }
    public virtual void ShowArea(int _area)
    {
    }

    #region Events
    public virtual void OnLevelStart()
    {
        OnLevelStartPanelEvent?.Invoke();
    }
    public virtual void OnSuccessState()
    {
        OnSuccessLevelEvent?.Invoke();
    }
    #endregion
}
