using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    protected Vector2 _tempTargetPos = Vector2.zero;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        this._playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMoveInput();
    }

    public virtual void physicsUpdate()
    {

    }

    public virtual void StateUpdate()
    {
        if (_playerStateMachine._player.stat.CurHp <= 0)
        {
            OnDie();
        }
    }

    protected virtual void AddInputActionsCallbacks()
    {
        if (_playerStateMachine != null && _playerStateMachine._player != null && _playerStateMachine._player._input != null)
        {
            PlayerInput input = _playerStateMachine._player._input;
            input._playerActions.Move.canceled += OnMoveCanceled;
            input._playerActions.NormalAttack.started += OnAttackStarted;
        }
        else
        {
            Debug.LogError("PlayerStateMachine 또는 Player 또는 PlayerInput이 null입니다.");
        }
    }


    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = _playerStateMachine._player._input;
        input._playerActions.Move.canceled -= OnMoveCanceled;
        input._playerActions.NormalAttack.started -= OnAttackStarted;
    }


    private void ReadMoveInput()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            _playerStateMachine.moveInput = Camera.main.ScreenToWorldPoint(_playerStateMachine._player._input._playerActions.Move.ReadValue<Vector2>());
            _tempTargetPos = _playerStateMachine.moveInput;
        }
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if(_playerStateMachine.moveInput == Vector2.zero)
        {
            return;
        }

        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
    }



    protected virtual void  OnMove()
    {
        
    }

    protected virtual void OnDie()
    {
        _playerStateMachine.ChangeState(_playerStateMachine._playerDieState);
    }
}
