using UnityEngine;

public class PlayerManager : CustomBehaviour
{
    #region Fields
    [SerializeField] private Player m_Player;
    #endregion
    public override void Initialize(GameManager _gameManager)
    {
        base.Initialize(_gameManager);
        m_Player.Initialize(this);

        GameManager.OnMainMenuEvent += OnMainMenu;
        GameManager.OnGameStartEvent += OnGameStart;
        GameManager.OnSuccessEvent += OnSuccess;
        GameManager.OnFailedEvent += OnFailed;
    }

    #region Events
    private void OnMainMenu()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.MainState, true);
    }
    private void OnGameStart()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.GamePlayState);
    }
    private void OnSuccess()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.SuccessState);
    }
    private void OnFailed()
    {
        m_Player.PlayerStateMachine.ChangeState(PlayerStates.FailedState);
    }
    private void OnDestroy()
    {
        GameManager.OnMainMenuEvent -= OnMainMenu;
        GameManager.OnGameStartEvent -= OnGameStart;
        GameManager.OnSuccessEvent -= OnSuccess;
        GameManager.OnFailedEvent -= OnFailed;
    }
    #endregion
}
