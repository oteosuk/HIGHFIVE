using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : MonsterBaseState
{
    public MonsterMoveState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        Move();
    }
    private void Move()
    {
        Vector2 playerPos = RangeInPlayer();
        Vector2 currentPos = _monsterStateMachine._monster.transform.position;

        Vector2 direction = (playerPos - currentPos).normalized;
        _monsterStateMachine._monster.Rigidbody.velocity = direction * _monsterStateMachine._monster.stat.MoveSpeed; 
    }
}
