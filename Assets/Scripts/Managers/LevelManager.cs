using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelManager : CustomBehaviour
{
    #region Events
    public event Action OnCleanSceneObject;
    public event Action OnSpawnedLetters;
    public event Action OnSetLettersParents;
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

        OnSpawnedLetters?.Invoke();
        OnSetLettersParents?.Invoke();
        WordManager.SpawnEmptyLetter();
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

