using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("AttackState");
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        OnAttack();
    }

    private void OnAttack()
    {
        float distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;

        if (distance > _monsterStateMachine._monster.stat.AttackRange || distance > _monsterStateMachine._monster.stat.SightRange)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
    }
}
