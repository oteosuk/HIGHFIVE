using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private float _attackTimer;
    private int _attackHash;
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_attackHash == 0)
        {
            _attackHash = _playerStateMachine._player.PlayerAnimationData.AttackParameterHash;
        }
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine.moveSpeedModifier = 0f;
        _playerStateMachine.isAttackReady = false;
        StartAnimation(_attackHash);
        Debug.Log(_playerStateMachine.targetObject.name);
        Debug.Log("Attack");
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        _attackTimer = 0.0f;
        StopAnimation(_attackHash);
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
