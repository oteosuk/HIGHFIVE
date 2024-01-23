using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    private float _attackTimer = 0.0f;
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine.moveSpeedModifier = 0f;
        Debug.Log("Attack");
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        _attackTimer = 0.0f;
        Debug.Log("Attack Exit");
        // 애니메이션 해제
    }

    //공격중
    public override void StateUpdate()
    {
        base.StateUpdate();
        //타겟 위치를 base에서 관리 해줘야함
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mousePoint = _playerStateMachine.moveInput;
            Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

            RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 100.0f);

            if (hit.collider?.gameObject != null)
            {
                int mask = 1 << hit.collider.gameObject.layer;
                if (mask == LayerMask.GetMask("Monster"))
                {
                    float distanced = (hit.collider.transform.position - _playerStateMachine._player.transform.position).magnitude;
                    if (_playerStateMachine._player.stat.AttackRange > distanced)
                    {
                        _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
                    }
                }
            }
            else
            {
                OnMove();
            }
        }

        float distance = (_targetObject.transform.position - _playerStateMachine._player.transform.position).magnitude;
        if (_playerStateMachine._player.stat.AttackRange < distance)
        {
            Debug.Log(distance);
            Debug.Log(_playerStateMachine._player.stat.AttackRange);
            _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }

}
