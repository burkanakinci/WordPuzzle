using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WordManager : CustomBehaviour<LevelManager>
{
    #region Events
    public event Action<bool> OnChangedClickedWord;
    public event Action<bool> OnSubmitWord;
    #endregion
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
        m_TempClickedWord = "";
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
    }
    public void AddClickableLetterList(Letter _clickable)
    {
        m_ClickableLetters.Add(_clickable);
    }
    private List<string> m_TargetLetters;
    private TextAsset m_TargetTextAsset;
    private string m_TargetLoadPath;
    private string m_TempClickedWord;
    public void ManageClickedLetterList(Letter _clicked, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_ClickedLetters.Contains(_clicked))
                {
                    m_ClickedLetters.Add(_clicked);
                    m_TempClickedWord += _clicked.LetterChar;
                }
                break;
            case (ListOperations.Substraction):
                if (m_ClickedLetters.Contains(_clicked))
                {
                    m_ClickedLetters.Remove(_clicked);
                    m_TempClickedWord = m_TempClickedWord.Replace(_clicked.LetterChar.ToString(), "");
                }
                break;
        }
        OnChangedClickedWord?.Invoke(m_ClickedLetters.Count == GameManager.Instance.Entities.EmptyLetterOnSceneCount);
    }

    public bool m_IsCorrectWord;
    public void CheckWord()
    {
        m_TargetLoadPath = "Texts/" + m_ClickedLetters[0].LetterChar + "/" + m_ClickedLetters[0].LetterChar + "_" + GameManager.Instance.Entities.EmptyLetterOnSceneCount;
        m_TargetTextAsset = (TextAsset)Resources.Load(m_TargetLoadPath);
        m_TargetLetters = m_TargetTextAsset.text.Split("\n").ToList();

        for (int _targetCount = 0; _targetCount < m_TargetLetters.Count; _targetCount++)
        {
            if (m_TargetLetters[_targetCount].Trim() == m_TempClickedWord.Trim())
            {
                OnSubmitWord?.Invoke(true);
                return;
            }
        }

        OnSubmitWord?.Invoke(false);
    }
}
