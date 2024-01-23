using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMoveState : PlayerBaseState
{
    private Vector2 _targetPosition;
    private float _moveSpeed;
    private int _moveHash;

    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_moveHash == 0)
        {
            _moveHash = _playerStateMachine._player.PlayerAnimationData.MoveParameterHash;
        }
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine.moveSpeedModifier = 1.0f;
        StartAnimation(_moveHash);
        Debug.Log("Move Enter");        
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Move Exit");
        StopAnimation(_moveHash);
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Move();
    }

    private void Move()
    { 
        if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
        {
            _targetPosition = GetMoveDirection();
            _moveSpeed = GetMoveSpeed();
        }
        _playerStateMachine._player.transform.position = Vector3.MoveTowards(_playerStateMachine._player.transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

        if (_playerStateMachine.targetObject != null)
        {
            float distance = (_playerStateMachine.targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (_playerStateMachine._player.stat.AttackRange > distance)
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
            }
        }
        else
        {
            int maxColliders = 10;
            Collider[] hitColliders = new Collider[maxColliders];
            int numColliders = Physics.OverlapSphereNonAlloc(_playerStateMachine._player.transform.position, _playerStateMachine._player.stat.AttackRange, hitColliders);
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].gameObject.tag == Enum.GetName(typeof(Define.Tag), Define.Tag.Enemy))
                {
                    _playerStateMachine.targetObject = hitColliders[i].gameObject;
                    _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
                }
            }
        }

        if (_targetPosition == (Vector2)_playerStateMachine._player.transform.position)
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        }
    }

    private float GetMoveSpeed()
    {
        float movementSpeed = _playerStateMachine._player.stat.MoveSpeed * _playerStateMachine.moveSpeedModifier;
        return movementSpeed;
    }

    private Vector2 GetMoveDirection()
    {
        Vector2 input = _playerStateMachine.moveInput;
        return new Vector2(input.x, input.y);
    }
}
