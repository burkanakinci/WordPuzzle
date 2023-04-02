using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmptyLetter : PooledObject
{
    [SerializeField] private Transform m_VisualParent;
    [SerializeField] private EmptyLetterData m_EmptyLetterData;
    private int m_EmptyLetterIndex;
    public override void Initialize()
    {
        base.Initialize();
        m_EmptyLetterSpawnSequenceID = GetInstanceID() + "m_EmptyWordSpawnSequenceID";
    }
    public override void OnObjectSpawn()
    {
        GameManager.Instance.Entities.ManageEmptyLetterOnScene(this, ListOperations.Adding);
        m_VisualParent.localScale = Vector3.zero;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        GameManager.Instance.Entities.ManageEmptyLetterOnScene(this, ListOperations.Substraction);
        base.OnObjectDeactive();
    }

    public void SetEmptyLetterIndex(int _index)
    {
        m_EmptyLetterIndex = _index;
    }

    private string m_EmptyLetterSpawnSequenceID;
    private Sequence m_EmptyLetterSequence;
    public void EmptyLetterSpawnSequence()
    {
        DOTween.Kill(m_EmptyLetterSpawnSequenceID);
        m_EmptyLetterSequence = DOTween.Sequence().SetId(m_EmptyLetterSpawnSequenceID);

        m_EmptyLetterSequence.Append(m_VisualParent.DOScale(Vector3.one, m_EmptyLetterData.SpawnTweenDuration).SetEase(m_EmptyLetterData.SpawnTweenEase));
        m_EmptyLetterSequence.InsertCallback(m_EmptyLetterData.RightSpawnDelay,
        () =>
        {
            if (GameManager.Instance.Entities.GetEmptyLetter(m_EmptyLetterIndex + 1) != null)
            {
                GameManager.Instance.Entities.GetEmptyLetter(m_EmptyLetterIndex + 1).EmptyLetterSpawnSequence();
            }
        });
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_EmptyLetterSpawnSequenceID);
    }
}
