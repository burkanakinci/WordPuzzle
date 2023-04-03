using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using DG.Tweening;

public class Entities : CustomBehaviour
{
    private Dictionary<int, Letter> m_LetterOnScene;
    private List<EmptyLetter> m_EmptyLetterOnScene;
    [SerializeField] private Transform[] m_ActiveParents;
    [SerializeField] private Score[] m_CharactesScores;

    #region EcternalAccess
    public int LetterOnSceneCount => m_LetterOnScene.Count;
    public int EmptyLetterOnSceneCount => m_EmptyLetterOnScene.Count;
    #endregion
    public override void Initialize()
    {
        base.Initialize();
        m_LetterOnScene = new Dictionary<int, Letter>();
        m_EmptyLetterOnScene = new List<EmptyLetter>();
    }

    #region Getters
    public Letter GetLetterByID(int _letterID)
    {
        if (m_LetterOnScene.ContainsKey(_letterID))
        {
            return m_LetterOnScene[_letterID];
        }
        else
        {
            return null;
        }
    }
    public EmptyLetter GetFirstEmptyLetter()
    {
        for (int _emptyCount = 0; _emptyCount < m_EmptyLetterOnScene.Count; _emptyCount++)
        {
            if (m_EmptyLetterOnScene[_emptyCount].LetterOnEmptyLetter == null)
            {
                return m_EmptyLetterOnScene[_emptyCount];
            }
        }

        return null;
    }
    public EmptyLetter GetEmptyLetterByIndex(int _index)
    {
        if (_index < m_EmptyLetterOnScene.Count)
        {
            return m_EmptyLetterOnScene[_index];
        }
        return null;
    }
    public Transform GetActiveParent(ActiveParents _parent)
    {
        if ((int)_parent < m_ActiveParents.Length)
        {
            return m_ActiveParents[(int)_parent];
        }
        else
        {
            return null;
        }
    }
    public int GetScore(char _letterChar)
    {
        for (int _scoreCount = 0; _scoreCount < m_CharactesScores.Length; _scoreCount++)
        {
            for (int _charCount = 0; _charCount < m_CharactesScores[_scoreCount].Character.Length; _charCount++)
            {
                if (m_CharactesScores[_scoreCount].Character[_charCount].ToString().ToUpper()[0] == _letterChar)
                {
                    return m_CharactesScores[_scoreCount].ScorePoint;
                }
            }
        }
        return 0;
    }
    #endregion
    #region Setters
    public void ManageLetterOnScene(int _letterID, Letter _letter, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_LetterOnScene.ContainsKey(_letterID))
                {
                    m_LetterOnScene.Add(_letterID, _letter);
                }
                break;
            case (ListOperations.Substraction):
                if (m_LetterOnScene.ContainsKey(_letterID))
                {
                    m_LetterOnScene.Remove(_letterID);
                }
                break;
        }
    }
    public void ManageEmptyLetterOnScene(EmptyLetter _emptyLetter, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_EmptyLetterOnScene.Contains(_emptyLetter))
                {
                    m_EmptyLetterOnScene.Add(_emptyLetter);
                    m_EmptyLetterOnScene = m_EmptyLetterOnScene.OrderBy(_empty => _empty.transform.position.x).ToList();
                }
                break;
            case (ListOperations.Substraction):
                if (m_EmptyLetterOnScene.Contains(_emptyLetter))
                {
                    m_EmptyLetterOnScene.Remove(_emptyLetter);
                }
                break;
        }
    }
    #endregion

    public void SetEmptyLetterIndexByEntities()
    {
        for (int _emptyCount = 0; _emptyCount < m_EmptyLetterOnScene.Count; _emptyCount++)
        {
            m_EmptyLetterOnScene[_emptyCount].SetEmptyLetterIndex(_emptyCount);
        }
    }
}