using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


[CreateAssetMenu(fileName = "WordData", menuName = "Word Data")]
public class WordData : ScriptableObject
{

    #region Datas

    [Header("Spawn Tween Values")]
    [SerializeField] private float m_SpawnTweenDuration;
    [SerializeField] private Ease m_SpawnTweenEase = Ease.Linear;
    [SerializeField] private float m_RightSpawnDelay;


    [Header("Click Tween Values")]
    [SerializeField] private float m_ClickTweenDuration;
    [SerializeField] private Ease m_ClickTweenEase = Ease.Linear;
    #endregion


    #region ExternalAccess

    #region SpawnTweenValues
    public float SpawnTweenDuration => m_SpawnTweenDuration;
    public Ease SpawnTweenEase => m_SpawnTweenEase;
    public float RightSpawnDelay => m_RightSpawnDelay;
    #endregion

    #region ClickTweenValues
    public float ClickTweenDuration => m_ClickTweenDuration;
    public Ease ClickTweenEase => m_ClickTweenEase;
    #endregion

    #endregion
}
