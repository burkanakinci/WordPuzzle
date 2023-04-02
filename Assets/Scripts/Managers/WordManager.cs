using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WordManager : CustomBehaviour<LevelManager>
{
    private List<Letter> m_ClickableLetters;
    private List<Letter> m_ClickedLetters;

    #region EcternalAccess
    public int ClickedCount => m_ClickedLetters.Count;
    #endregion
    public override void Initialize(LevelManager _levelManager)
    {
        base.Initialize(_levelManager);
        m_ClickableLetters = new List<Letter>();
        m_ClickedLetters = new List<Letter>();
        m_TargetLetters = new List<string>();
    }
    public void AddClickableLetterList(Letter _clickable)
    {
        m_ClickableLetters.Add(_clickable);
    }
    private List<string> m_TargetLetters;
    private TextAsset m_TargetTextAsset;
    private string m_TargetLoadPath;
    public void AddClickedLetterList(Letter _clicked)
    {
        m_ClickedLetters.Add(_clicked);

        if (GameManager.Instance.Entities.EmptyLetterOnSceneCount == m_ClickedLetters.Count)
        {
            m_TargetLoadPath = "Texts/" + m_ClickedLetters[0].LetterChar + "/" + m_ClickedLetters[0].LetterChar + "_" + GameManager.Instance.Entities.EmptyLetterOnSceneCount;
            m_TargetTextAsset = (TextAsset)Resources.Load(m_TargetLoadPath);
            m_TargetLetters = m_TargetTextAsset.text.Split("\n").ToList();
        }
    }

    private Vector3 m_TempEmptyLetterSpawnPos;
    public void SpawnEmptyLetter()
    {
        m_TempEmptyLetterSpawnPos = (m_ClickableLetters.Count % 2 == 0) ? (Vector3.right * 2.0f) : (Vector3.zero);
        for (int _emptyLetterCount = 0; _emptyLetterCount < m_ClickableLetters.Count; _emptyLetterCount++)
        {
            if (_emptyLetterCount % 2 == 1)
            {
                m_TempEmptyLetterSpawnPos.x = ((m_TempEmptyLetterSpawnPos.x * -1.0f) + 4.0f);
            }
            else
            {
                m_TempEmptyLetterSpawnPos.x = (m_TempEmptyLetterSpawnPos.x * -1.0f);
            }

            GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.EMPTY_LETTER),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveEmptyLetterParent).TransformPoint(m_TempEmptyLetterSpawnPos)),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveEmptyLetterParent)));
        }

        GameManager.Instance.Entities.SetEmptyLetterIndexByEntities();
        GameManager.Instance.Entities.GetEmptyLetter(0).EmptyLetterSpawnSequence();
    }
}
