using System.Collections;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private bool _canAttack = true;

    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_animData.AttackParameterHash);
        Debug.Log("Attack Enter");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.AttackParameterHash);
        Debug.Log("Attack Exit");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        AttackRangeCheck();
    }

    private void AttackRangeCheck()
    {
        float distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;

        if (distance > _monsterStateMachine._monster.stat.AttackRange)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
    }
}
