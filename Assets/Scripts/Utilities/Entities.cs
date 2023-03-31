using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using DG.Tweening;

public class Entities : CustomBehaviour
{
    [SerializeField] private Transform[] m_ActiveParents;
    public override void Initialize()
    {
        base.Initialize();
    }

    #region Getters
    public Transform GetActiveParent(ActiveParents _parent)
    {
        return m_ActiveParents[(int)_parent];
    }
    #endregion
}