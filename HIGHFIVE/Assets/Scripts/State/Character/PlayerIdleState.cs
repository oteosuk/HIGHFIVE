using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) :  base(playerStateMachine)
    {

    }

    public override void Enter()
    {
        //_playerStateMachine._moveSpeedModifier = 0f;
        base.Enter();
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("Idle");
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
}