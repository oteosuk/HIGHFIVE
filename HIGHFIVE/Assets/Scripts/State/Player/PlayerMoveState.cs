using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("Move");
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = GetMoveDirection();

        float moveSpeed = GetMoveSpeed();
        _playerStateMachine._player.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection.x < 0)
            _playerStateMachine._player.transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDirection.x > 0)
            _playerStateMachine._player.transform.localScale = new Vector3(1, 1, 1);
    }

    private float GetMoveSpeed()
    {
        float movementSpeed = _playerStateMachine._moveSpeed * _playerStateMachine._moveSpeedModifier;
        return movementSpeed;
    }

    private Vector3 GetMoveDirection()
    {
        Vector2 input = _playerStateMachine.moveInput;
        return new Vector3(input.x, input.y, 0f).normalized;
    }
}
