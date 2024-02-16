using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfuseState : PlayerBaseState
{
    private int _confuseHash;
    public PlayerConfuseState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_confuseHash == 0)
        {
            _confuseHash = _playerStateMachine._player.PlayerAnimationData.ConfuseParameterHash;
        }
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        StartAnimation(_confuseHash);
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
        StopAnimation(_confuseHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
