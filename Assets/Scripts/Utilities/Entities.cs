using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using DG.Tweening;

public class Entities : CustomBehaviour
{
    private Dictionary<int, Word> m_WordOnScene;
    private List<EmptyWord> m_EmptyWordOnScene;
    [SerializeField] private Transform[] m_ActiveParents;
    public override void Initialize()
    {
        base.Initialize();
        m_WordOnScene = new Dictionary<int, Word>();
        m_EmptyWordOnScene = new List<EmptyWord>();
    }

    #region Getters
    public Word GetWordByID(int _wordID)
    {
        if (m_WordOnScene.ContainsKey(_wordID))
        {
            return m_WordOnScene[_wordID];
        }
        else
        {
            return null;
        }
    }
    public EmptyWord GetEmptyWord(int _index)
    {
        if (_index < m_EmptyWordOnScene.Count)
        {
            return m_EmptyWordOnScene[_index];
        }
        else
        {
            return null;
        }
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
    #endregion
    #region Setters
    public void ManageWordOnSceneList(int _wordID, Word _word, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_WordOnScene.ContainsKey(_wordID))
                {
                    m_WordOnScene.Add(_wordID, _word);
                }
                break;
            case (ListOperations.Substraction):
                if (m_WordOnScene.ContainsKey(_wordID))
                {
                    m_WordOnScene.Remove(_wordID);
                }
                break;
        }
    }
    public void ManageEmptyWordList(EmptyWord _emptyWord, ListOperations _operation)
    {
        switch (_operation)
        {
            case (ListOperations.Adding):
                if (!m_EmptyWordOnScene.Contains(_emptyWord))
                {
                    m_EmptyWordOnScene.Add(_emptyWord);
                    m_EmptyWordOnScene = m_EmptyWordOnScene.OrderBy(_empty => _empty.transform.position.x).ToList();
                }
                break;
            case (ListOperations.Substraction):
                if (m_EmptyWordOnScene.Contains(_emptyWord))
                {
                    m_EmptyWordOnScene.Remove(_emptyWord);
                }
                break;
        }
    }
    #endregion

    public void SetEmptyWordsIndex()
    {
        for (int _emptyCount = 0; _emptyCount < m_EmptyWordOnScene.Count; _emptyCount++)
        {
            m_EmptyWordOnScene[_emptyCount].SetEmptyWordIndex(_emptyCount);
        }
    }
}