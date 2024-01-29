using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
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
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_attackHash);
        // 애니메이션 해제
    }

    //공격중
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (CheckTargetInRange()) {  OnAttack(); }
        else { OnMove(); }
    }

    private void OnAttack()
    {
        _playerStateMachine._player.OnNormalAttack();
    }

    private bool CheckTargetInRange()
    {
        if (_playerStateMachine.targetObject != null)
        {
            float distance = (_playerStateMachine.targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (distance > _playerStateMachine._player.stat.AttackRange)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }
    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }
}
