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
    [SerializeField] private Transform[] m_ActiveParents;
    public override void Initialize()
    {
        base.Initialize();
        m_WordOnScene = new Dictionary<int, Word>();
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
    public Transform GetActiveParent(ActiveParents _parent)
    {
        return m_ActiveParents[(int)_parent];
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
    #endregion
}