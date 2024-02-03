using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        _playerStateMachine._player.SetLayer((int)Define.Layer.Default);
        _playerStateMachine._player.Collider.isTrigger = true;
        StartAnimation(_playerStateMachine._player.PlayerAnimationData.DieParameterHash);
        
        // 기능
        base.Enter();
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
        StopAnimation(_playerStateMachine._player.PlayerAnimationData.DieParameterHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
