using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordManager : CustomBehaviour<LevelManager>
{
    private List<Word> m_ClickableWords;
    #region Events
    public event Action OnFilledClickableWordsEvent;
    #endregion 
    public override void Initialize(LevelManager _levelManager)
    {
        base.Initialize(_levelManager);
        m_ClickableWords = new List<Word>();
        OnFilledClickableWordsEvent += SpawnEmptyWord;
    }
    public void AddClickableWordList(Word _clickable)
    {
        m_ClickableWords.Add(_clickable);
    }

    private Vector3 m_TempEmptyWordSpawnPos;
    private void SpawnEmptyWord()
    {
        m_TempEmptyWordSpawnPos = (m_ClickableWords.Count % 2 == 0) ? (Vector3.right * 2.0f) : (Vector3.zero);
        for (int _emptyWordCount = 0; _emptyWordCount < m_ClickableWords.Count; _emptyWordCount++)
        {
            if (_emptyWordCount % 2 == 1)
            {
                m_TempEmptyWordSpawnPos.x = ((m_TempEmptyWordSpawnPos.x * -1.0f) + 4.0f);
            }
            else
            {
                m_TempEmptyWordSpawnPos.x = (m_TempEmptyWordSpawnPos.x * -1.0f);
            }

            GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.EMPTY_WORD),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveEmptyWordParent).TransformPoint(m_TempEmptyWordSpawnPos)),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveEmptyWordParent)));
        }
    }

    #region Events
    public void OnFilledClickableList()
    {
        OnFilledClickableWordsEvent?.Invoke();
    }
    #endregion
}
