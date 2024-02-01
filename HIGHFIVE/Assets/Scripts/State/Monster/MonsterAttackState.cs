using System.Collections;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    //private bool _canAttack = true;
    private float _distance;
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
        if (_monsterStateMachine.targetObject != null)
        {
            _distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;
            if (_distance > _monsterStateMachine._monster.stat.AttackRange)
            {
                _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
            }
        }
        else
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
    }
}
