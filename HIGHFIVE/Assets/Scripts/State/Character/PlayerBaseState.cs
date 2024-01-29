using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
//using UnityEngine.UIElements;

public class PlayerBaseState : MonoBehaviour, IState
{
    protected PlayerStateMachine _playerStateMachine;

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

    public virtual void PhysicsUpdate()
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
        if (_playerStateMachine != null && _playerStateMachine._player != null && _playerStateMachine._player.Input != null)
        {
            PlayerInput input = _playerStateMachine._player.Input;
            input._playerActions.Move.canceled += OnMoveCanceled;
            input._playerActions.NormalAttack.started += OnReadyAttackStart;
        }
        else
        {
            Debug.LogError("PlayerStateMachine 또는 Player 또는 PlayerInput이 null입니다.");
        }
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = _playerStateMachine._player.Input;
        input._playerActions.Move.canceled -= OnMoveCanceled;
        input._playerActions.NormalAttack.started -= OnReadyAttackStart;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if(_playerStateMachine.moveInput == Vector2.zero)
        {
            return;
        }

        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    }

    protected virtual void  OnMove() { }

    protected virtual void OnDie()
    {
        _playerStateMachine.ChangeState(_playerStateMachine._playerDieState);
    }

    protected virtual void StartAnimation(int hashValue)
    {
        _playerStateMachine._player.PlayerAnim.SetBool(hashValue, true);
    }

    protected virtual void StopAnimation(int hashValue)
    {
        _playerStateMachine._player.PlayerAnim.SetBool(hashValue, false);
    }
    private void OnReadyAttackStart(InputAction.CallbackContext context) { _playerStateMachine.isAttackReady = true; }

    private void RayToObjectAndSetTarget()
    {
        Vector2 mousePoint = _playerStateMachine._player.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red ));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 100.0f, mask);

        if (hit.collider?.gameObject != null)
        {
            _playerStateMachine.targetObject = hit.collider.gameObject;
            float distance = (hit.collider.transform.position - _playerStateMachine._player.transform.position).magnitude;

            if (_playerStateMachine._player.stat.AttackRange > distance)
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
            }
            else
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
            }
        }
        else
        {
            Debug.Log("dd");
            _playerStateMachine.targetObject = null;
        }

        _playerStateMachine.moveInput = Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void ReadMoveInput()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RayToObjectAndSetTarget();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame && _playerStateMachine.isAttackReady)
        {
            RayToObjectAndSetTarget();
        }
    }
}
