using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


[CreateAssetMenu(fileName = "EmptyWordData", menuName = "Empty Word Data")]
public class EmptyWordData : ScriptableObject
{

    #region Datas

    [Header("Spawn Tween Values")]
    [SerializeField] private float m_SpawnTweenDuration;
    [SerializeField] private Ease m_SpawnTweenEase=Ease.Linear;

    #endregion


    #region ExternalAccess

    #region SpawnTweenValues
    public float SpawnTweenDuration => m_SpawnTweenDuration;
    public Ease SpawnTweenEase => m_SpawnTweenEase;
    #endregion

    #endregion
}
