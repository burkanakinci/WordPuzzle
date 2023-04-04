using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SuccessPanel : UIPanel
{
    [SerializeField] private HighScoreArea m_HighScoreArea;
    public override void Initialize(UIManager _uiManager)
    {
        base.Initialize(_uiManager);
        OnSuccessLevelEvent += () =>
        {
            m_HighScoreArea.ShowArea();
        };
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

    }
    private Coroutine m_MainStateCoroutine;
    private void StartMainStateCoroutine()
    {
        if (m_MainStateCoroutine != null)
        {
            StopCoroutine(m_MainStateCoroutine);
        }
        m_MainStateCoroutine = StartCoroutine(MainStateCoroutine());
    }
    private IEnumerator MainStateCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.OnMainMenu();
    }
}


