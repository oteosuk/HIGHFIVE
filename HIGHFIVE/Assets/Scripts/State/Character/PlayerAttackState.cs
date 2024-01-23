using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private float _attackTimer = 0.0f;
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine.moveSpeedModifier = 0f;
        Debug.Log("Attack");
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        _attackTimer = 0.0f;
        Debug.Log("Attack Exit");
        // 애니메이션 해제
    }

    //공격중
    public override void StateUpdate()
    {
        base.StateUpdate();

        if (_playerStateMachine.targetObject == null)
        {
            OnMove();
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
}
