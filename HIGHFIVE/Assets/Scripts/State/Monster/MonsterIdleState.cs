using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle  In");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Idle  Exit");
    }
    public override void StateUpdate()
    {
        Vector2 playerPos = RangeInPlayer();
        if (playerPos != Vector2.zero)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
            Debug.Log("Move들어가나?");
        }
        else
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
            Debug.Log("Idle들어가나?");
        }

    }

    protected override void OnMove()
    {
        base.OnMove();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
    }
    protected override void OnDie()
    {
        base.OnDie();
    }

    protected Vector2 RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red"));
        Debug.Log(playerCollider);
        Debug.Log(LayerMask.GetMask("Red"));
        return playerCollider != null ? playerCollider.transform.position : Vector2.zero;
    }
}
