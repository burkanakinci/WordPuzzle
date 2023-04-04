using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HighScoreArea : BaseArea<SuccessPanel>
{
    [Header("Area Fields")]
    [SerializeField] private TextMeshProUGUI m_HighScoreText;


    [Header("Tween Values")]
    [SerializeField] private float m_ScoreTextShowDuration;
    [SerializeField] private Ease m_ScoreTextShowEase;
    [SerializeField] private float m_ScoreTextHideDuration;
    [SerializeField] private Ease m_ScoreTextHideEase;
    [SerializeField] private float m_HideWordScoreDelayDuration;
    public override void Initialize(SuccessPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        GameManager.Instance.PlayerManager.OnAddedHighScoreEvent += OnAddedHighScore;
        m_StartShowScoreTweenID = GetInstanceID() + "m_StartShowScoreTweenID";
        m_HideWordScoreDelayID = GetInstanceID() + "m_HideWordScoreDelayID";
        m_HighScoreColor = m_HighScoreText.color;
        m_HighScoreColor.a = 0.0f;
        m_HighScoreText.color = m_HighScoreColor;
    }

    private void OnAddedHighScore(int _score)
    {
        m_HighScoreText.text = "New High Score/n" + _score;
        ShowHighScoreTween();
    }
    private float m_HighScoreLerpValue;
    private string m_StartShowScoreTweenID;
    private void ShowHighScoreTween()
    {
        DOTween.Kill(m_StartShowScoreTweenID);
        m_HighScoreLerpValue = 0.0f;
        DOTween.To(() => m_HighScoreLerpValue, x => m_HighScoreLerpValue = x, 1.0f, m_ScoreTextShowDuration)
        .OnUpdate(() => SetWordScoreAlphaColor(m_HighScoreLerpValue))
        .OnComplete(() => StartHideWordScoreTween())
        .SetEase(m_ScoreTextShowEase)
        .SetId(m_StartShowScoreTweenID);
    }
    private Color m_HighScoreColor;
    private void SetWordScoreAlphaColor(float _lerpValue)
    {
        m_HighScoreColor.a = Mathf.Lerp(0.0f, 1.0f, _lerpValue);
        m_HighScoreText.color = m_HighScoreColor;
    }

    private string m_HideWordScoreDelayID;
    private void StartHideWordScoreTween()
    {
        DOTween.Kill(m_HideWordScoreDelayID);
        DOVirtual.DelayedCall(m_HideWordScoreDelayDuration, () => HideWordScoreTween())
        .SetId(m_HideWordScoreDelayID);
    }
    private void HideWordScoreTween()
    {
        DOTween.Kill(m_StartShowScoreTweenID);
        m_HighScoreLerpValue = 1.0f;
        DOTween.To(() => m_HighScoreLerpValue, x => m_HighScoreLerpValue = x, 0.0f, m_ScoreTextHideDuration)
        .OnUpdate(() => SetWordScoreAlphaColor(m_HighScoreLerpValue))
        .SetEase(m_ScoreTextHideEase)
        .SetId(m_StartShowScoreTweenID);
    }
}




