using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WordManager : CustomBehaviour<LevelManager>
{
    #region Events
    public event Action<int, int> OnChangedClickedWordEvent;
    public event Action<bool, string> OnSubmitWord;
    public event Action<int> OnIncreaseScoreEvent;
    #endregion
    private List<Letter> m_ClickableLetters;
    private List<Letter> m_ClickedLetters;

    #region EcternalAccess
    public int ClickedCount => m_ClickedLetters.Count;
    public int ClickableCount=>m_ClickableLetters.Count;
    #endregion
    public override void Initialize(LevelManager _levelManager)
    {
        base.Initialize(_levelManager);
        OnSubmitWord += OnIncreaseScore;
        m_ClickableLetters = new List<Letter>();
        m_ClickedLetters = new List<Letter>();
        m_TargetLetters = new List<string>();
        TempClickedWord = "";
    }

    private Vector3 m_TempEmptyLetterSpawnPos;
    private string m_TempTargetWord;

    public void StartSpawnEmptyLetters()
    {
        GameManager.Instance.AutoSolver.StartCheckWord(m_TempTargetWord);
    }

    private string m_TempEmptyWord;
    public void SpawnEmptyLetters()
    {
        m_TempEmptyWord = GameManager.Instance.AutoSolver.TempClickableTarget;

        m_TempEmptyLetterSpawnPos = (m_TempEmptyWord.Length % 2 == 0) ? (Vector3.right * 2.0f) : (Vector3.zero);
        for (int _emptyLetterCount = 0; _emptyLetterCount < m_TempEmptyWord.Length; _emptyLetterCount++)
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
        GameManager.Instance.Entities.GetFirstEmptyLetter().EmptyLetterSpawnSequence();
    }
    public void ManageClickableList(Letter _clickable, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_ClickableLetters.Contains(_clickable))
                {
                    m_ClickableLetters.Add(_clickable);
                    m_TempTargetWord += _clickable.LetterChar;
                }
                break;
            case (ListOperations.Substraction):
                if (m_ClickableLetters.Contains(_clickable))
                {
                    m_ClickableLetters.Remove(_clickable);
                    m_TempTargetWord = m_TempTargetWord.Replace(_clickable.LetterChar.ToString(), "");
                }
                break;
        }
    }
    private List<string> m_TargetLetters;
    private TextAsset m_TargetTextAsset;
    private string m_TargetLoadPath;
    public string TempClickedWord { get; private set; }
    public void ManageClickedLetterList(Letter _clicked, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_ClickedLetters.Contains(_clicked))
                {
                    m_ClickedLetters.Add(_clicked);
                    TempClickedWord += _clicked.LetterChar;
                }
                break;
            case (ListOperations.Substraction):
                if (m_ClickedLetters.Contains(_clicked))
                {
                    m_ClickedLetters.Remove(_clicked);
                    TempClickedWord = TempClickedWord.Replace(_clicked.LetterChar.ToString(), "");
                }
                break;
        }
        OnChangedClickedWordEvent?.Invoke(m_ClickedLetters.Count, GameManager.Instance.Entities.EmptyLetterOnSceneCount);
    }
    public Letter GetLastClickedLetter()
    {
        return m_ClickedLetters[m_ClickedLetters.Count - 1];
    }

    public void OnSubmit(bool _isCorrect)
    {
        OnSubmitWord?.Invoke(_isCorrect, TempClickedWord);
    }

    private void OnIncreaseScore(bool _isIncrease, string _word)
    {
        if (_isIncrease)
        {
            m_TempIncreaseScore = (m_TempCharacterPointTotal * 10 * _word.Length);
            OnIncreaseScoreEvent?.Invoke(m_TempIncreaseScore);
        }
    }

    private int m_TempIncreaseScore;
    private int m_TempCharacterPointTotal;
    public void IncreaseScore(int _increaseValue)
    {
        m_TempCharacterPointTotal += _increaseValue;
    }
    public void DecreaseScore(int _decreaseValue)
    {
        m_TempCharacterPointTotal -= _decreaseValue;
    }
    private void OnDestroy()
    {
        OnSubmitWord -= OnIncreaseScore;
    }
}
