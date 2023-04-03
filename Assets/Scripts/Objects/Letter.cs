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
        m_CorrectWordSequenceID = GetInstanceID() + "m_CorrectWordSequenceID";
    }
    public override void OnObjectSpawn()
    {
        this.gameObject.layer = (int)ObjectsLayer.Letter;
        m_LetterVisual.localScale = Vector3.one;
        GameManager.Instance.LevelManager.OnSpawnedLettersEvent += OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnCompletedLetterParentsEvent += OnCompletedManageParents;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        GameManager.Instance.LevelManager.WordManager.ManageClickedLetterList(this, ListOperations.Substraction);
        GameManager.Instance.Entities.ManageLetterOnScene(m_CurrentTile.id, this, ListOperations.Substraction);
        GameManager.Instance.LevelManager.OnSpawnedLettersEvent -= OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnCompletedLetterParentsEvent -= OnCompletedManageParents;
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

    public void ManageParentList(Letter _letter, ListOperations _opeation)
    {
        switch (_opeation)
        {
            case (ListOperations.Adding):
                if (!m_LetterParents.Contains(_letter))
                    m_LetterParents.Add(_letter);
                break;
            case (ListOperations.Substraction):
                if (m_LetterParents.Contains(_letter))
                    m_LetterParents.Remove(_letter);
                break;
        }
    }
    public void SetLetterSelectableStatus()
    {
        if (m_LetterParents.Count > 0)
        {
            this.gameObject.layer = (int)ObjectsLayer.Default;
            m_LetterBGSprite.sortingOrder = (int)SpriteOrderInLayer.LetterSpriteDeactive;
            m_LetterText.sortingOrder = (int)SpriteOrderInLayer.LetterTextDeactive;
            m_LetterBGSprite.color = m_CannotSelectableStatusColor;
            m_LetterText.color = m_CannotSelectableStatusColor;
        }
        else
        {
            this.gameObject.layer = (int)ObjectsLayer.Letter;
            m_LetterBGSprite.sortingOrder = (int)SpriteOrderInLayer.LetterSpriteActive;
            m_LetterText.sortingOrder = (int)SpriteOrderInLayer.LetterTextActive;
            m_LetterBGSprite.color = m_SelecetableStatusColor;
            m_LetterText.color = m_SelecetableStatusColor;
            GameManager.Instance.LevelManager.WordManager.AddClickableLetterList(this);
        }
    }

    public void ClickedLetter()
    {
        ClickSequence();
    }

    private EmptyLetter m_EmptyLetterOnClicked;
    private string m_LetterClickSequenceID;
    private Sequence m_LetterClickSequence;
    private void ClickSequence()
    {
        this.gameObject.layer = (int)ObjectsLayer.Default;
        DOTween.Kill(m_LetterClickSequenceID);
        m_LetterClickSequence = DOTween.Sequence().SetId(m_LetterClickSequenceID);
        m_EmptyLetterOnClicked = GameManager.Instance.Entities.GetFirstEmptyLetter();
        m_EmptyLetterOnClicked.ManageLetterOnEmptyLetter(this, ListOperations.Adding);
        GameManager.Instance.LevelManager.WordManager.ManageClickedLetterList(this, ListOperations.Adding);
        m_LetterClickSequence.Append(transform.DOMove(m_EmptyLetterOnClicked.transform.position, m_LetterData.ClickTweenDuration));
        m_LetterClickSequence.Join(transform.DOScale((Vector3.one * (2.0f / 3.0f)), m_LetterData.ClickTweenDuration));
        m_LetterClickSequence.AppendCallback(() =>
        {
            GameManager.Instance.InputManager.SetInputCanClickable(true);
        });
    }

    private void ManageChildsParent(ListOperations _operation)
    {
        for (int _childCount = 0; _childCount < m_CurrentTile.children.Length; _childCount++)
        {
            GameManager.Instance.Entities.GetLetterByID(m_CurrentTile.children[_childCount]).ManageParentList(this, _operation);
        }
    }

    private string m_CorrectWordSequenceID;
    private Sequence m_CorrectWordSequence;
    public void CorrectWordSequence(ref TextMeshPro _emptyText)
    {
        _emptyText.text = LetterChar.ToString();
        KillAllTween();
        m_CorrectWordSequence = DOTween.Sequence().SetId(m_CorrectWordSequenceID);
        m_CorrectWordSequence.Append(m_LetterVisual.DOScale(m_LetterData.CorrectWordScaleUpValue, m_LetterData.CorrectWordScaleUpDuration).SetEase(m_LetterData.CorrectWordScaleUpEase));
        m_CorrectWordSequence.Append(m_LetterVisual.DOScale(Vector3.zero, m_LetterData.CorrectWordScaleDownDuration).SetEase(m_LetterData.CorrectWordScaleDownEase));
        m_CorrectWordSequence.AppendCallback(() =>
        {
            m_EmptyLetterOnClicked.ManageLetterOnEmptyLetter(this, ListOperations.Substraction);
            GameManager.Instance.ObjectPool.SpawnFromPool(
                (PooledObjectTags.CORRECT_LETTER_VFX),
                (transform.position),
                (Quaternion.identity),
                (GameManager.Instance.Entities.GetActiveParent(ActiveParents.ActiveVFXParent))
            );
            OnObjectDeactive();
        });
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_CorrectWordSequenceID);
        DOTween.Kill(m_LetterClickSequenceID);
    }

    #region Events
    private void OnSpawnedLetters()
    {
        ManageChildsParent(ListOperations.Adding);
    }
    private void OnCompletedManageParents()
    {
        SetLetterSelectableStatus();
    }
    private void OnDestroy()
    {
        KillAllTween();
        GameManager.Instance.LevelManager.OnSpawnedLettersEvent -= OnSpawnedLetters;
        GameManager.Instance.LevelManager.OnCompletedLetterParentsEvent -= OnCompletedManageParents;
    }
    #endregion
}
