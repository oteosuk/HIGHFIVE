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
        _playerStateMachine._moveSpeedModifier = 0f;
        Debug.Log("Idle");
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

        if(_playerStateMachine.moveInput != Vector2.zero)
        {
            OnMove();
            return;
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        Debug.Log("OnMove called");
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
}