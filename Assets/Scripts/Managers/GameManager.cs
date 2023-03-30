using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    #region Fields
    public JsonConverter JsonConverter;
    public PlayerManager PlayerManager;
    public UIManager UIManager;
    public LevelManager LevelManager;
    public ObjectPool ObjectPool;
    public Entities Entities;
    public InputManager InputManager;
    #endregion
    #region Actions
    public event Action OnMainMenuEvent;
    public event Action OnGameStartEvent;
    public event Action OnSuccessEvent;
    public event Action OnFailedEvent;
    #endregion

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        InitializeGameManager();
    }
    private void Start()
    {
        OnMainMenu();
    }

    public void InitializeGameManager()
    {
        ObjectPool.Initialize(this);
        JsonConverter.Initialize(this);
        PlayerManager.Initialize(this);
        LevelManager.Initialize(this);
        Entities.Initialize(this);
        InputManager.Initialize(this);
        UIManager.Initialize(this);
    }

    #region Events
    public void OnMainMenu()
    {
        OnMainMenuEvent?.Invoke();
    }
    public void OnGameStart()
    {
        OnGameStartEvent?.Invoke();
    }
    public void OnSuccess()
    {
        OnSuccessEvent?.Invoke();
    }
    public void OnFailed()
    {
        OnFailedEvent?.Invoke();
    }
    #endregion
}