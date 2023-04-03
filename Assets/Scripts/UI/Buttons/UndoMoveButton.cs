using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoMoveButton : UIBaseButton<HudArea>
{
    [Header("Button Images")]
    [SerializeField] private Image m_UndoButtonBG;
    [SerializeField] private Image m_UndoButtonIcon;

    [Header("Button Status Colors")]
    [SerializeField] private Color m_BGDeactiveColor;
    [SerializeField] private Color m_BGActiveColor;
    [SerializeField] private Color m_IconDeactiveColor;
    [SerializeField] private Color m_IconActiveColor;
    public override void Initialize(HudArea _cachedComponent)
    {
        base.Initialize(_cachedComponent);
        GameManager.Instance.LevelManager.WordManager.OnChangedClickedWordEvent += SetCheckWordButtonStatus;
        SetCheckWordButtonStatus(-1, 1);
    }
    protected override void OnClickAction()
    {
        GameManager.Instance.InputManager.SetInputCanClickable(false);
        GameManager.Instance.LevelManager.WordManager.GetLastClickedLetter().MoveUndoLetter();
    }
    #region Event
    public void SetCheckWordButtonStatus(int _clickedCount, int _emptyCount)
    {
        m_UndoButtonBG.color = (_clickedCount > 0) ? m_BGActiveColor : m_BGDeactiveColor;
        m_UndoButtonIcon.color = (_clickedCount > 0) ? m_IconActiveColor : m_IconDeactiveColor;
        m_Button.enabled = (_clickedCount > 0);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.Instance.LevelManager.WordManager.OnChangedClickedWordEvent -= SetCheckWordButtonStatus;
    }
    #endregion
}
