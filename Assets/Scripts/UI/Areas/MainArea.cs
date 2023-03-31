using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainArea : BaseArea<MainMenuPanel>
{
    [SerializeField] private OpenLevelAreaButton m_OpenLevelAreaButton;

    [Header("On Tween Fields")]
    [SerializeField] private float m_LevelsButtonDelayedDuration;

    public override void Initialize(MainMenuPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_LevelsButtonDelayedCallID = GetInstanceID() + "m_LevelsButtonDelayedCallID";

        m_OpenLevelAreaButton.Initialize(this);
        HideArea();
    }

    public override void ShowArea()
    {
        base.ShowArea();
        StartLevelButtonSequence();
    }

    private string m_LevelsButtonDelayedCallID;
    private void StartLevelButtonSequence()
    {
        DOTween.Kill(m_LevelsButtonDelayedCallID);
        DOVirtual.DelayedCall((m_LevelsButtonDelayedDuration), () =>
            {
                m_OpenLevelAreaButton.LevelButtonShowSequence();
            })
            .SetId(m_LevelsButtonDelayedCallID);
    }
    private void KillAllTween()
    {
        DOTween.Kill(m_LevelsButtonDelayedCallID);
    }
    public override void HideArea()
    {
        KillAllTween();
        m_OpenLevelAreaButton.transform.localScale = Vector3.zero;
        m_OpenLevelAreaButton.DisableLevelStartButton();
        base.HideArea();
    }
}
