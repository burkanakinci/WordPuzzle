using UnityEngine;

public class UIPanel : CustomBehaviour<UIManager>
{
    [SerializeField] private CanvasGroup m_CanvasGroup;
    public CanvasGroup CanvasGroup => m_CanvasGroup;

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
}
