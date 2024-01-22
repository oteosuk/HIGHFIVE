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
        //타겟 위치를 base에서 관리 해줘야함
        if (Mouse.current.rightButton.isPressed)
        {
            OnMove();
        }

        //플레이어의 공격속도를 가져와서 그 초가 다 끝났을 때 ChangeState

        _attackTimer += Time.deltaTime;

        if (_attackTimer > 1.0f / 2)
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }

}
