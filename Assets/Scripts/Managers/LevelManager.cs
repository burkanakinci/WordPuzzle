using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : CustomBehaviour
{
    #region Events
    public event Action OnCleanSceneObject;
    public event Action OnSpawnedWords;
    public event Action OnSetWordsParents;
    #endregion
    private LevelData m_CurrentLevelData;
    public LevelData CurrentLevelData => m_CurrentLevelData;
    public WordManager WordManager;
    public override void Initialize()
    {
        base.Initialize();
        WordManager.Initialize(this);
    }
    public void StartLevel(LevelData _levelData)
    {
        m_CurrentLevelData = _levelData;

        GameManager.Instance.OnLevelStart();
    }
    private Word m_TempSpawnedWord;
    private void SpawnLevelObjects()
    {
        SpawnLevelWords();
    }
    private void SpawnLevelWords()
    {
        for (int _tileCount = 0; _tileCount < m_CurrentLevelData.tiles.Length; _tileCount++)
        {
            m_TempSpawnedWord = GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.WORD),
                (m_CurrentLevelData.tiles[_tileCount].position),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveWordParent))
            ).GetGameObject().GetComponent<Word>();

            m_TempSpawnedWord.SetWordData(m_CurrentLevelData.tiles[_tileCount]);
        }

        OnSpawnedWords?.Invoke();
        OnSetWordsParents?.Invoke();
        WordManager.OnFilledClickableList();
    }
    #region Getter
    public TileData GetTile(int _tileId)
    {
        return m_CurrentLevelData.tiles[_tileId];
    }
    #endregion

    #region Events

    public void OnMainMenu()
    {

    }
    public void OnLevelStart()
    {
        SpawnLevelObjects();
    }
    public void OnExitGameplay()
    {
        OnCleanSceneObject?.Invoke();
    }
    #endregion
}

