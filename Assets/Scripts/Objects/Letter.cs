using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class Letter : PooledObject
{
    [Header("Visual Fields")]
    [SerializeField] private Color m_SelecetableStatusColor;
    [SerializeField] private Color m_CannotSelectableStatusColor;


    [Header("Letter Fields")]
    [SerializeField] private Transform m_LetterVisual;
    [SerializeField] private SpriteRenderer m_LetterBGSprite;
    [SerializeField] private TextMeshPro m_LetterText;
    [SerializeField] private LetterData m_LetterData;


    private List<Letter> m_LetterParents;
    private TileData m_CurrentTile;
    public char LetterChar { get; private set; }
    public override void Initialize()
    {
        base.Initialize();
        m_LetterParents = new List<Letter>();
        m_LetterClickSequenceID = GetInstanceID() + "m_WordClickSequenceID";
    }
    public override void OnObjectSpawn()
    {
        GameManager.Instance.LevelManager.OnSpawnedLetters += OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnSetLettersParents += OnSetLetterParents;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.Entities.ManageLetterOnScene(m_CurrentTile.id, this, ListOperations.Substraction);
        GameManager.Instance.LevelManager.OnSpawnedLetters -= OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnSetLettersParents -= OnSetLetterParents;
        m_LetterParents.Clear();
        base.OnObjectDeactive();
    }
    public void SetLetterData(TileData _tile)
    {
        m_CurrentTile = _tile;

        LetterChar = m_CurrentTile.character.ToUpper()[0];
        m_LetterText.text = LetterChar.ToString();

        GameManager.Instance.Entities.ManageLetterOnScene(m_CurrentTile.id, this, ListOperations.Adding);
    }

    public void AddParentList(Letter _letter)
    {
        if (!m_LetterParents.Contains(_letter))
            m_LetterParents.Add(_letter);
    }

    private void AddLetterChildrenList()
    {
        for (int _childCount = 0; _childCount < m_CurrentTile.children.Length; _childCount++)
        {
            GameManager.Instance.Entities.GetLetterByID(m_CurrentTile.children[_childCount]).AddParentList(this);
        }
    }
    private void SetLetterSelectableStatus()
    {
        if (m_LetterParents.Count > 0)
        {
            m_LetterBGSprite.color = m_CannotSelectableStatusColor;
            m_LetterText.color = m_CannotSelectableStatusColor;
        }
        else
        {
            GameManager.Instance.LevelManager.WordManager.AddClickableLetterList(this);
            m_LetterBGSprite.color = m_SelecetableStatusColor;
            m_LetterText.color = m_SelecetableStatusColor;
        }
    }

    public void ClickedLetter()
    {
        ClickSequence();
    }

    private string m_LetterClickSequenceID;
    private Sequence m_LetterClickSequence;
    private void ClickSequence()
    {
        DOTween.Kill(m_LetterClickSequenceID);
        m_LetterClickSequence = DOTween.Sequence().SetId(m_LetterClickSequenceID);
        m_LetterClickSequence.Append(transform.DOMove(GameManager.Instance.Entities.GetEmptyLetter(GameManager.Instance.LevelManager.WordManager.ClickedCount).transform.position, m_LetterData.ClickTweenDuration));
        m_LetterClickSequence.Join(transform.DOScale((Vector3.one * (2.0f / 3.0f)), m_LetterData.ClickTweenDuration));
        m_LetterClickSequence.AppendCallback(() =>
        {
            GameManager.Instance.LevelManager.WordManager.AddClickedLetterList(this);
            GameManager.Instance.InputManager.SetInputCanClickable(true);
        });
    }

    #region Events
    private void OnSpawnedLetters()
    {
        AddLetterChildrenList();
    }
    private void OnSetLetterParents()
    {
        SetLetterSelectableStatus();
    }
    private void OnDestroy()
    {
        GameManager.Instance.LevelManager.OnSpawnedLetters -= OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnSetLettersParents -= OnSetLetterParents;
    }
    #endregion
}
