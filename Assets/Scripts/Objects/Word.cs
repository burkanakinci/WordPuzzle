using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Word : PooledObject
{
    [Header("Visual Fields")]
    [SerializeField] private Color m_SelecetableStatusColor;
    [SerializeField] private Color m_CannotSelectableStatusColor;


    [Header("Word Fields")]
    [SerializeField] private Transform m_WordVisual;
    [SerializeField] private SpriteRenderer m_WordBGSprite;
    [SerializeField] private TextMeshPro m_WordLetterText;


    private List<Word> m_WordParents;
    private TileData m_CurrentTile;
    private char m_WordLetter;
    public override void Initialize()
    {
        base.Initialize();
        m_WordParents = new List<Word>();
    }
    public override void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnSpawnedWords += OnSpawnedWords;
        GameManager.Instance.LevelManager.OnSetWordsParents += OnSetWordsParents;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.Entities.ManageWordOnSceneList(m_CurrentTile.id, this, ListOperations.Substraction);
        GameManager.Instance.LevelManager.OnSpawnedWords -= OnSpawnedWords;
        GameManager.Instance.LevelManager.OnSetWordsParents -= OnSetWordsParents;
        m_WordParents.Clear();
        base.OnObjectDeactive();
    }
    public void SetWordData(TileData _tile)
    {
        m_CurrentTile = _tile;

        m_WordLetter = m_CurrentTile.character.ToUpper()[0];
        m_WordLetterText.text = m_WordLetter.ToString();

        GameManager.Instance.Entities.ManageWordOnSceneList(m_CurrentTile.id, this, ListOperations.Adding);
    }

    public void AddParentList(Word _word)
    {
        if (!m_WordParents.Contains(_word))
            m_WordParents.Add(_word);
    }

    private void AddedWordChildrenList()
    {
        for (int _childCount = 0; _childCount < m_CurrentTile.children.Length; _childCount++)
        {
            GameManager.Instance.Entities.GetWordByID(m_CurrentTile.children[_childCount]).AddParentList(this);
        }
    }
    private void SetWordSelectableStatus()
    {
        if (m_WordParents.Count > 0)
        {
            m_WordBGSprite.color = m_CannotSelectableStatusColor;
            m_WordLetterText.color = m_CannotSelectableStatusColor;
        }
        else
        {
            GameManager.Instance.LevelManager.WordManager.AddClickableWordList(this);
            m_WordBGSprite.color = m_SelecetableStatusColor;
            m_WordLetterText.color = m_SelecetableStatusColor;
        }
    }

    public void ClickedWord()
    {

    }

    #region Events
    private void OnSpawnedWords()
    {
        AddedWordChildrenList();
    }
    private void OnSetWordsParents()
    {
        SetWordSelectableStatus();
    }
    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.OnSpawnedWords -= OnSpawnedWords;
        GameManager.Instance.LevelManager.OnSetWordsParents -= OnSetWordsParents;
    }
    #endregion
}
