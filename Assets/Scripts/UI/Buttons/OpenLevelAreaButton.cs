using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OpenLevelAreaButton : UIBaseButton<MainArea>
{

    [Header("On Tween Fields")]
    [SerializeField] private float m_LevelsButtonGrowthDuration;
    [SerializeField] private float m_LevelsButtonShrinkageDuration;

    private string m_LevelsButtonScaleSequenceID;
    private Sequence m_LevelsButtonShowSequence;
    public override void Initialize(MainArea _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        m_LevelButtonScaleTweenID = GetInstanceID() + "m_LevelButtonScaleTweenID";
        m_LevelsButtonScaleSequenceID = GetInstanceID() + "m_LevelsButtonScaleSequenceID";
    }

    protected override void OnClickAction()
    {
        LevelButtonScaleTween((Vector3.zero), (m_LevelsButtonGrowthDuration), (Ease.OutExpo), (() =>
        {
            CachedComponent.CachedComponent.HideAllArea();
            CachedComponent.CachedComponent.ShowArea((int)MainMenuAreas.LevelPopupArea);
        }));
    }
    private string m_LevelButtonScaleTweenID;
    private Tween LevelButtonScaleTween(Vector3 _scale, float _duration, Ease ease = Ease.Linear, TweenCallback _onCompleteCallback = null)
    {
        DOTween.Kill(m_LevelButtonScaleTweenID);
        return transform.DOScale
        ((_scale),
        (m_LevelsButtonGrowthDuration))
        .SetEase(Ease.OutExpo)
        .SetId(m_LevelButtonScaleTweenID)
        .OnComplete(() => _onCompleteCallback?.Invoke());
    }
    public void LevelButtonShowSequence()
    {
        DOTween.Kill(m_LevelsButtonScaleSequenceID);
        m_LevelsButtonShowSequence = DOTween.Sequence().SetId(m_LevelsButtonScaleSequenceID);
        m_LevelsButtonShowSequence.Append(LevelButtonScaleTween((Vector3.one * 1.18f), (m_LevelsButtonGrowthDuration), (Ease.OutExpo)));
        m_LevelsButtonShowSequence.Append(LevelButtonScaleTween((Vector3.one), (m_LevelsButtonShrinkageDuration), (Ease.InExpo)));
        m_LevelsButtonShowSequence.AppendCallback(() =>
        {
            EnableLevelStartButton();
        });

    }

    private void KillAllTween()
    {
        DOTween.Kill(m_LevelButtonScaleTweenID);
        DOTween.Kill(m_LevelsButtonScaleSequenceID);
    }

    public override void DisableLevelStartButton()
    {
        KillAllTween();
        base.DisableLevelStartButton();
    }

    protected override void OnDestroy()
    {
        KillAllTween();
        base.OnDestroy();
    }
}
