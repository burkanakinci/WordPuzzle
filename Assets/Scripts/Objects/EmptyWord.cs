using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmptyWord : PooledObject
{
    [SerializeField] private Transform m_VisualParent;
    [SerializeField] private EmptyWordData m_EmptyWordData;
    public override void Initialize()
    {
        base.Initialize();
        m_EmptyWordSpawnSequenceID = GetInstanceID() + "m_EmptyWordSpawnSequenceID";
    }
    public override void OnObjectSpawn()
    {
        GameManager.Instance.Entities.ManageEmptyWordList(this, ListOperations.Adding);
        m_VisualParent.localScale = Vector3.zero;
        EmptyWordSpawnSequence();
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        GameManager.Instance.Entities.ManageEmptyWordList(this, ListOperations.Substraction);
        base.OnObjectDeactive();
    }

    private string m_EmptyWordSpawnSequenceID;
    private Sequence m_EmptyWordSequence;
    private void EmptyWordSpawnSequence()
    {
        DOTween.Kill(m_EmptyWordSpawnSequenceID);
        m_EmptyWordSequence = DOTween.Sequence().SetId(m_EmptyWordSpawnSequenceID);

        m_EmptyWordSequence.Append(m_VisualParent.DOScale(Vector3.one, m_EmptyWordData.SpawnTweenDuration).SetEase(m_EmptyWordData.SpawnTweenEase));
    }
}
