using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private int _attackHash;
    private bool isFistTime = true;
    private double _curDelay;

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
        _playerStateMachine._player.NavMeshAgent.isStopped = true;
        _playerStateMachine._player.Animator.SetFloat("AttackSpeed", _playerStateMachine._player.stat.AttackSpeed);
        _playerStateMachine.moveInput = _playerStateMachine._player.transform.position;
        _playerStateMachine.moveSpeedModifier = 0f;
        _playerStateMachine.isAttackReady = false;
        _curDelay = _playerStateMachine._player.stat.AttackDelay / _playerStateMachine._player.stat.AttackSpeed;
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
        if (CheckTargetInRange())
        {
            _curDelay -= Time.deltaTime;
            if (_curDelay <= 0)
            {
                _playerStateMachine._player.OnNormalAttack();
                isFistTime = false;
                _curDelay = 1.0 / _playerStateMachine._player.stat.AttackSpeed;
            }
        }
    }

    private bool CheckTargetInRange()
    {
        if (_playerStateMachine._player.targetObject != null && _playerStateMachine._player.targetObject.layer != (int)Define.Layer.Default)
        {
            if (isFistTime) return true;
            float distance = (_playerStateMachine._player.targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (distance > _playerStateMachine._player.stat.AttackRange)
            {
                OnMove();
                return false;
            }
            else
            {
                return true;
            }
        }
        OnIdle();
        return false;
    }
    protected override void OnMove()
    {
        base.OnMove();
        isFistTime = true;
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }

    private void OnIdle()
    {
        isFistTime = true;
        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }
}
