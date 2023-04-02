using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;


[CreateAssetMenu(fileName = "EmptyLetterData", menuName = "Empty Letter Data")]
public class EmptyLetterData : ScriptableObject
{

    #region Datas

    [Header("Spawn Tween Values")]
    [SerializeField] private float m_SpawnTweenDuration;
    [SerializeField] private Ease m_SpawnTweenEase = Ease.Linear;
    [SerializeField] private float m_RightSpawnDelay;

    #endregion


    #region ExternalAccess

    #region SpawnTweenValues
    public float SpawnTweenDuration => m_SpawnTweenDuration;
    public Ease SpawnTweenEase => m_SpawnTweenEase;
    public float RightSpawnDelay => m_RightSpawnDelay;
    #endregion

    #endregion
}
