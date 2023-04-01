using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmptyWord : PooledObject
{
    [SerializeField] private Transform m_VisualParent;
    [SerializeField] private EmptyWordData m_EmptyWordData;
    private int m_EmptyWordIndex;
    public override void Initialize()
    {
        base.Initialize();
        m_EmptyWordSpawnSequenceID = GetInstanceID() + "m_EmptyWordSpawnSequenceID";
    }
    public override void OnObjectSpawn()
    {
        GameManager.Instance.Entities.ManageEmptyWordList(this, ListOperations.Adding);
        m_VisualParent.localScale = Vector3.zero;
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        KillAllTween();
        GameManager.Instance.Entities.ManageEmptyWordList(this, ListOperations.Substraction);
        base.OnObjectDeactive();
    }

    public void SetEmptyWordIndex(int _index)
    {
        m_EmptyWordIndex = _index;
    }

    private string m_EmptyWordSpawnSequenceID;
    private Sequence m_EmptyWordSequence;
    public void EmptyWordSpawnSequence()
    {
        DOTween.Kill(m_EmptyWordSpawnSequenceID);
        m_EmptyWordSequence = DOTween.Sequence().SetId(m_EmptyWordSpawnSequenceID);

        m_EmptyWordSequence.Append(m_VisualParent.DOScale(Vector3.one, m_EmptyWordData.SpawnTweenDuration).SetEase(m_EmptyWordData.SpawnTweenEase));
        m_EmptyWordSequence.InsertCallback(m_EmptyWordData.RightSpawnDelay,
        () =>
        {
            if (GameManager.Instance.Entities.GetEmptyWord(m_EmptyWordIndex + 1) != null)
            {
                GameManager.Instance.Entities.GetEmptyWord(m_EmptyWordIndex + 1).EmptyWordSpawnSequence();
            }
        });
    }

    private void KillAllTween()
    {
        DOTween.Kill(m_EmptyWordSpawnSequenceID);
    }
}
