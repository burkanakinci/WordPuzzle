
using System.Collections.Generic;
public class Player : CustomBehaviour<PlayerManager>
{
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public override void Initialize(PlayerManager _playerManager)
    {
        base.Initialize(_playerManager);

        PlayerStateMachine=new PlayerStateMachine(this);
    }
}
