using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager : CustomBehaviour
{
    [SerializeField] private LayerMask m_ClickableLayerMask;
    private bool m_IsUIOverride;
    private bool m_CanClickable;
    public event Action OnClicked;
    public event Action OnClickedUp;
    public override void Initialize()
    {
        base.Initialize();
        OnClicked += SetMatchableRay;
    }
    public void SetInputCanClickable(bool _canClickable)
    {
        m_CanClickable = _canClickable;
    }
    private void Update()
    {
        UpdateUIOverride();

        if (Input.GetMouseButtonDown(0) && !m_IsUIOverride)
        {
            OnClicked?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0) && !m_IsUIOverride)
        {
            OnClickedUp?.Invoke();
        }
    }


    public void UpdateUIOverride()
    {
        m_IsUIOverride = EventSystem.current.IsPointerOverGameObject();
    }

    private RaycastHit m_ClickableHit;
    private Ray m_ClickableRay;
    private Letter m_TempClickedLetter;
    private void SetMatchableRay()
    {
        m_ClickableRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(m_ClickableRay, out m_ClickableHit, Mathf.Infinity, m_ClickableLayerMask))
        {
            SetInputCanClickable(false);
            m_TempClickedLetter = m_ClickableHit.collider.gameObject.GetComponent<Letter>();
            m_TempClickedLetter.ClickedLetter();
        }
    }

    #region Events

    private void OnMainMenu()
    {

    }
    private void OnGameStart()
    {
    }
    private void OnDestroy()
    {
    }
    #endregion
}
