using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWordButton : UIBaseButton<HudArea>
{
    [Header("Button Images")]
    [SerializeField] private Image m_CheckWordButtonBG;
    [SerializeField] private Image m_CheckWordButtonIcon;

    [Header("Button Status Colors")]
    [SerializeField] private Color m_BGDeactiveColor;
    [SerializeField] private Color m_BGActiveColor;
    [SerializeField] private Color m_IconDeactiveColor;
    [SerializeField] private Color m_IconActiveColor;
    public override void Initialize(HudArea _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }
    protected override void OnClickAction()
    {

    }
}
