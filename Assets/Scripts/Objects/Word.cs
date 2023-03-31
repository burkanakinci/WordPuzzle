using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Word : PooledObject
{
    [Header("Word Fields")]
    [SerializeField] private Transform m_WordVisual;
    [SerializeField] private TextMeshPro m_WordLetterText;

    private TileData m_CurrentTile;
    private char m_WordLetter;
    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
    }
    public override void OnObjectDeactive()
    {
        base.OnObjectDeactive();
    }
    public void SetWordData(TileData _tile)
    {
        m_CurrentTile = _tile;

        m_WordLetter = m_CurrentTile.character.ToUpper()[0];
        m_WordLetterText.text = m_WordLetter.ToString();
    }
}
