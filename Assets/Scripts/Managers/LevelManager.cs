using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : CustomBehaviour
{
    #region Events
    public event Action OnCleanSceneObjectEvent;
    public event Action OnSpawnedLettersEvent;
    public event Action OnCompletedLetterParentsEvent;
    #endregion
    private LevelData m_CurrentLevelData;
    public LevelData CurrentLevelData => m_CurrentLevelData;
    public WordManager WordManager;
    private int m_LevelScore;
    public override void Initialize()
    {
        base.Initialize();
        GameManager.Instance.LevelManager.WordManager.OnIncreaseScoreEvent += OnIncreaseScore;
        WordManager.Initialize(this);
    }
    public void StartLevel(LevelData _levelData)
    {
        m_CurrentLevelData = _levelData;

        GameManager.Instance.OnLevelStart();
    }
    private Letter m_TempSpawnedLetter;
    private void SpawnLevelObjects()
    {
        SpawnLevelLetters();
    }
    private void SpawnLevelLetters()
    {
        for (int _tileCount = 0; _tileCount < m_CurrentLevelData.tiles.Length; _tileCount++)
        {
            m_TempSpawnedLetter = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.LETTER),
                (m_CurrentLevelData.tiles[_tileCount].position),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveLetterParent))
            ).GetGameObject().GetComponent<Letter>();

            m_TempSpawnedLetter.SetLetterData(m_CurrentLevelData.tiles[_tileCount]);
        }
    }
    #region Getter
    public TileData GetTile(int _tileId)
    {
        return m_CurrentLevelData.tiles[_tileId];
    }
    #endregion

    #region Events

    public void OnIncreaseScore(int _wordScore)
    {
        m_LevelScore += _wordScore;
    }
    public void OnLevelStart()
    {
        m_LevelScore = 0;
        SpawnLevelObjects();
        OnSpawnedLettersEvent?.Invoke();
        OnCompletedLetterParentsEvent?.Invoke();
    }
    public void OnExitGameplay()
    {
        OnCleanSceneObjectEvent?.Invoke();
    }
    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.WordManager.OnIncreaseScoreEvent -= OnIncreaseScore;
    }
    #endregion
}

