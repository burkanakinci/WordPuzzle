using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArea<T> : CustomBehaviour<T>
{
    [SerializeField] private CanvasGroup m_AreaCanvasGroup;
    public override void Initialize(T _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }

    public virtual void ShowArea()
    {
        m_AreaCanvasGroup.Open();
    }

    public virtual void HideArea()
    {
        m_AreaCanvasGroup.Close();
    }
}
