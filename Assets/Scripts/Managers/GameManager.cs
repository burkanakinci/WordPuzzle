using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    public event Action OnLevelStartEvent;
    public event Action OnSuccessEvent;
    public event Action OnFailedEvent;
    #endregion

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Instance = this;

        InitializeGameManager();
    }
    private void Start()
    {
        OnMainMenu();
    }

    public void InitializeGameManager()
    {
        ObjectPool.Initialize();
        JsonConverter.Initialize();
        Entities.Initialize();
        PlayerManager.Initialize();
        LevelManager.Initialize();
        UIManager.Initialize();
        InputManager.Initialize();
    }

    #region Events
    public void OnMainMenu()
    {
        OnMainMenuEvent?.Invoke();
    }
    public void OnLevelStart()
    {
        OnLevelStartEvent?.Invoke();
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