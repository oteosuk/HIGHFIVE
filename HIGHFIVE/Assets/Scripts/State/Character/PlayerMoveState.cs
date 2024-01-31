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
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
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
        if (Mouse.current.rightButton.isPressed)
        {
            _playerStateMachine.isLeftClicked = false;
            PrepareForMove();
        }
        else if (_playerStateMachine.isAttackReady && Mouse.current.leftButton.isPressed)
        {
            PrepareForMove();
            _playerStateMachine.isLeftClicked = true;
        }

        _targetPosition = _playerStateMachine._player.targetObject == null ? _playerStateMachine.moveInput : _playerStateMachine._player.targetObject.transform.position;
        MovePlayerToTarget();

        CheckForAttack();

        if (_targetPosition == (Vector2)_playerStateMachine._player.transform.position)
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        }
    }

    private void PrepareForMove()
    {
        _playerStateMachine.isAttackReady = false;
        _moveSpeed = _playerStateMachine._player.stat.MoveSpeed * _playerStateMachine.moveSpeedModifier;
    }

    private void CheckForAttack()
    {
        if (_playerStateMachine._player.targetObject != null)
        {
            float distance = (_playerStateMachine._player.targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (_playerStateMachine._player.stat.AttackRange > distance)
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
            }
        }
        else if (_playerStateMachine.isLeftClicked && _playerStateMachine._player.targetObject == null)
        {
            FindObjectInSight();
        }
    }

    private void MovePlayerToTarget()
    {
        _playerStateMachine._player.transform.position = Vector2.MoveTowards(
            _playerStateMachine._player.transform.position,
            _targetPosition,
            _moveSpeed * Time.deltaTime
        );
    }

    private void FindObjectInSight()
    {
        int monsterMask = LayerMask.GetMask("Monster");
        int enemyMask = Main.GameManager.SelectedCamp == Define.Camp.Red ? LayerMask.GetMask("Blue") : LayerMask.GetMask("Red");

        Collider2D monsterCollider = Physics2D.OverlapCircle(_playerStateMachine._player.transform.position, _playerStateMachine._player.stat.SightRange, monsterMask);
        Collider2D enemyCollider = Physics2D.OverlapCircle(_playerStateMachine._player.transform.position, _playerStateMachine._player.stat.SightRange, enemyMask);

        if (enemyCollider != null || monsterCollider != null)
        {
            _playerStateMachine._player.targetObject = enemyCollider != null ? enemyCollider.gameObject : monsterCollider.gameObject;
            _targetPosition = _playerStateMachine._player.targetObject.transform.position;
            _playerStateMachine.isLeftClicked = false;
        }
    }
}
