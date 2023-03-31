using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainArea : BaseArea<MainMenuPanel>
{
    [SerializeField] private OpenLevelAreaButton m_OpenLevelAreaButton;

    [Header("On Tween Fields")]
    [SerializeField] private float m_LevelsButtonDelayedDuration;
    [SerializeField] private float m_LevelsButtonGrowthDuration;
    [SerializeField] private float m_LevelsButtonShrinkageDuration;

    public override void Initialize(MainMenuPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_LevelsButtonDelayedCallID = GetInstanceID() + "m_LevelsButtonDelayedCallID";
        m_LevelsButtonSequenceID = GetInstanceID() + "m_LevelsButtonSequenceID";
        m_OpenLevelAreaButton.Initialize(this);
        HideArea();
    }

    private string m_LevelsButtonSequenceID;
    private Sequence m_LevelsButtonSequence;
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
                LevelButtonSequence();
            })
            .SetId(m_LevelsButtonDelayedCallID);
    }
    private void LevelButtonSequence()
    {
        DOTween.Kill(m_LevelsButtonSequenceID);
        m_LevelsButtonSequence = DOTween.Sequence().SetId(m_LevelsButtonSequenceID);
        m_LevelsButtonSequence.Append(m_OpenLevelAreaButton.transform.DOScale((Vector3.one * 1.18f), m_LevelsButtonGrowthDuration).SetEase(Ease.OutExpo));
        m_LevelsButtonSequence.Append(m_OpenLevelAreaButton.transform.DOScale((Vector3.one), m_LevelsButtonShrinkageDuration).SetEase(Ease.InExpo));
        m_LevelsButtonSequence.AppendCallback(() =>
        {
            m_OpenLevelAreaButton.EnableLevelStartButton();
        });
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_LevelsButtonDelayedCallID);
        DOTween.Kill(m_LevelsButtonSequenceID);
    }
    public override void HideArea()
    {
        KillAllTween();
        m_OpenLevelAreaButton.transform.localScale = Vector3.zero;
        m_OpenLevelAreaButton.DisableLevelStartButton();
        base.HideArea();
    }
}
