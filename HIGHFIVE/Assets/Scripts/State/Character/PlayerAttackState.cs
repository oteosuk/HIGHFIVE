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
        _playerStateMachine._player.Animator.SetFloat("AttackSpeed", _playerStateMachine._player.stat.AttackSpeed);

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
    }
}
