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
using UnityEngine.TextCore.Text;
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
        Character myCharacter = _playerStateMachine._player;
        HandlePlayerInput();

        _targetPosition = _playerStateMachine._player.targetObject == null ? _playerStateMachine.moveInput : _playerStateMachine._player.targetObject.transform.position;

        if (myCharacter.Animator.GetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash)) return;

        MovePlayerToTarget();
        
        if (_playerStateMachine.InputKey != Define.InputKey.FirstSkill && _playerStateMachine.InputKey != Define.InputKey.SecondSkill)
        {
            CheckForAttack();
        }
        

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

        Collider2D[] monsterCollider = Physics2D.OverlapCircleAll(_playerStateMachine._player.transform.position, _playerStateMachine._player.stat.SightRange, monsterMask);
        Collider2D[] enemyCollider = Physics2D.OverlapCircleAll(_playerStateMachine._player.transform.position, _playerStateMachine._player.stat.SightRange, enemyMask);

        
        if (enemyCollider.Length != 0 || monsterCollider.Length != 0)
        {
            GameObject targetObj = FindClosestObj(_playerStateMachine._player.transform.position, enemyCollider.Length != 0 ? enemyCollider : monsterCollider);
            _playerStateMachine._player.targetObject = targetObj;
            _targetPosition = _playerStateMachine._player.targetObject.transform.position;
            _playerStateMachine.isLeftClicked = false;
        }
    }

    private void HandlePlayerInput()
    {
        switch (_playerStateMachine.InputKey)
        {
            case Define.InputKey.RightMouse:
                HandleRightMouseInput();
                _playerStateMachine.InputKey = Define.InputKey.None;
                break;
            case Define.InputKey.LeftMouse:
                HandleLeftMouseInput();
                _playerStateMachine.InputKey = Define.InputKey.None;
                break;
            case Define.InputKey.FirstSkill:
                HandleFirstSkillInput();
                break;
            case Define.InputKey.SecondSkill:
                HandleSeconSkillInput();
                break;
        }
    }

    private void HandleRightMouseInput()
    {
        _playerStateMachine.isLeftClicked = false;
        PrepareForMove();
    }
    private void HandleLeftMouseInput()
    {
        _playerStateMachine.isLeftClicked = true;
        PrepareForMove();
    }
    private void HandleFirstSkillInput()
    {
        Character myCharacter = _playerStateMachine._player;
        if (myCharacter.CharacterSkill.FirstSkill.CanUseSkill())
        {
            _playerStateMachine.ChangeState(_playerStateMachine.PlayerFirstSkillState);
        }
    }
    private void HandleSeconSkillInput()
    {
        Character myCharacter = _playerStateMachine._player;
        if (myCharacter.CharacterSkill.SecondSkill.CanUseSkill())
        {
            _playerStateMachine.ChangeState(_playerStateMachine.PlayerSecondSkillState);
        }
    }
    GameObject FindClosestObj(Vector2 origin, Collider2D[] colliders)
    {
        float closestDistance = Mathf.Infinity;
        Collider2D closestCollider = null;

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(origin, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = collider;
            }
        }

        return closestCollider.gameObject;
    }
}
