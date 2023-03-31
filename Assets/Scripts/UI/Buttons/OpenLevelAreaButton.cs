using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLevelAreaButton : UIBaseButton<MainArea>
{
    public override void Initialize(MainArea _cachedComponent)
    {
        base.Initialize(_cachedComponent);
    }

    protected override void OnClickAction()
    {
        CachedComponent.CachedComponent.HideAllArea();
        CachedComponent.CachedComponent.ShowArea((int)MainMenuAreas.LevelPopupArea);
    }
}
