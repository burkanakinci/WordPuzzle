using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HudArea : BaseArea<HudPanel>
{
    [Header("Level Values")]
    [SerializeField] private TextMeshProUGUI m_LevelNumberText;
    [SerializeField] private TextMeshProUGUI m_LevelTitleText;


    [Header("Hud Buttons")]
    [SerializeField] private CheckWordButton m_CheckWordButton;
    [SerializeField] private UndoMoveButton m_UndoMoveButton;

    [Header("Matched Text")]
    [SerializeField] private TextMeshProUGUI m_MatchedText;


    [Header("Word Score Text")]
    [SerializeField] private TextMeshProUGUI m_WordScoreText;

    [Header("Tween Values")]
    [SerializeField] private float m_ScoreTextShowDuration;
    [SerializeField] private Ease m_ScoreTextShowEase;
    [SerializeField] private float m_ScoreTextHideDuration;
    [SerializeField] private Ease m_ScoreTextHideEase;
    [SerializeField] private float m_HideWordScoreDelayDuration;

    private Color m_WordScoreColor;
    public override void Initialize(HudPanel _cachedComponent)
    {
        base.Initialize(_cachedComponent);

        m_WordScoreColor = m_WordScoreText.color;
        m_WordScoreColor.a = 0.0f;
        m_WordScoreText.color = m_WordScoreColor;

        CachedComponent.OnLevelStartPanelEvent += SetLevelNumberText;
        CachedComponent.OnLevelStartPanelEvent += SetLevelTitleText;
        CachedComponent.OnLevelStartPanelEvent += ResetMatchedText;
        GameManager.Instance.LevelManager.WordManager.OnSubmitWord += OnSubmitWord;
        GameManager.Instance.LevelManager.WordManager.OnIncreaseScoreEvent += OnIncreaseScore;

        m_CheckWordButton.Initialize(this);
        m_UndoMoveButton.Initialize(this);

        m_ShowWordScoreTweenID = GetInstanceID() + "m_ShowWordScoreTweenID";
        m_HideWordScoreDelayID = GetInstanceID() + "m_HideWordScoreDelayID";
    }
    private float m_ShowWordScoreLerpValue;
    private string m_ShowWordScoreTweenID;
    private void ShowWordScoreTween()
    {
        DOTween.Kill(m_ShowWordScoreTweenID);
        m_ShowWordScoreLerpValue = 0.0f;
        DOTween.To(() => m_ShowWordScoreLerpValue, x => m_ShowWordScoreLerpValue = x, 1.0f, m_ScoreTextShowDuration)
        .OnUpdate(() => SetWordScoreAlphaColor(m_ShowWordScoreLerpValue))
        .OnComplete(() => StartHideWordScoreTween())
        .SetEase(m_ScoreTextShowEase)
        .SetId(m_ShowWordScoreTweenID);
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
        DOTween.Kill(m_ShowWordScoreTweenID);
        m_ShowWordScoreLerpValue = 1.0f;
        DOTween.To(() => m_ShowWordScoreLerpValue, x => m_ShowWordScoreLerpValue = x, 0.0f, m_ScoreTextHideDuration)
        .OnUpdate(() => SetWordScoreAlphaColor(m_ShowWordScoreLerpValue))
        .SetEase(m_ScoreTextHideEase)
        .SetId(m_ShowWordScoreTweenID);
    }

    private void SetWordScoreAlphaColor(float _lerpValue)
    {
        m_WordScoreColor.a = Mathf.Lerp(0.0f, 1.0f, _lerpValue);
        m_WordScoreText.color = m_WordScoreColor;
    }

    #region Events
    private void SetLevelNumberText()
    {
        m_LevelNumberText.text = "Level : " + GameManager.Instance.LevelManager.CurrentLevelData.LevelNumber;
    }
    private void SetLevelTitleText()
    {
        m_LevelTitleText.text = GameManager.Instance.LevelManager.CurrentLevelData.title;
    }
    private void ResetMatchedText()
    {
        m_MatchedText.text = "";
    }
    private void OnSubmitWord(bool _isCorrect, string _word)
    {
        if (_isCorrect)
        {
            m_MatchedText.text += _word;
            m_MatchedText.text += "\n";
        }
    }
    private void OnIncreaseScore(int _score)
    {
        ShowWordScoreTween();
        m_WordScoreText.text = "Word Score : " + _score.ToString();
    }
    #endregion
}
