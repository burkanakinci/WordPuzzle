using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

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
        GameManager.Instance.LevelManager.WordManager.OnSubmitWord += OnSubmitWord;
        WordManager.Initialize(this);
        m_NewWordDelayID = GetInstanceID() + "m_NewWordDelayID";
    }
    public void StartLevel(LevelData _levelData)
    {
        m_CurrentLevelData = _levelData;
        SpawnLevelObjects();
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
    private Coroutine m_NewWordCoroutine;
    private void StartNewWordCoroutine()
    {
        if (m_NewWordCoroutine != null)
        {
            StopCoroutine(m_NewWordCoroutine);
        }
        m_NewWordCoroutine = StartCoroutine(NewWordCoroutine());
    }
    private IEnumerator NewWordCoroutine()
    {
        yield return new WaitUntil(() => GameManager.Instance.Entities.EmptyLetterOnSceneCount == 0);
        NewWordDelayedCall();
    }
    private string m_NewWordDelayID;
    private void NewWordDelayedCall()
    {
        DOTween.Kill(m_NewWordDelayID);
        DOVirtual.DelayedCall(1.0f, () => OnLevelStart());
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_NewWordDelayID);
    }

    #region Events
    public void OnIncreaseScore(int _wordScore)
    {
        m_LevelScore += _wordScore;
    }
    public void OnLevelStart()
    {
        m_LevelScore = 0;
        OnSpawnedLettersEvent?.Invoke();
        OnCompletedLetterParentsEvent?.Invoke();
        WordManager.SpawnEmptyLetter();
        GameManager.Instance.Entities.SetEmptyLetterIndexByEntities();
        GameManager.Instance.Entities.GetFirstEmptyLetter().EmptyLetterSpawnSequence();
    }
    public void OnExitGameplay()
    {
        OnCleanSceneObjectEvent?.Invoke();
    }
    private void OnSubmitWord(bool _isCorrect, string _word)
    {
        if (_isCorrect)
        {
            StartNewWordCoroutine();
        }
    }
    private void OnDestroy()
    {
        KillAllTween();
        GameManager.Instance.LevelManager.WordManager.OnIncreaseScoreEvent -= OnIncreaseScore;
        GameManager.Instance.LevelManager.WordManager.OnSubmitWord -= OnSubmitWord;
    }
    #endregion
}

