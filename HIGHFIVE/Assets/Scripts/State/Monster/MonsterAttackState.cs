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
        Debug.Log("AttackState");
    }

    public override void Exit()
    {
        StopAnimation(_animData.AttackParameterHash);
        base.Exit();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        AttackRangeCheck();
    }

    /*void CanAttackAgain()
    {
        _canAttack = true;
    }*/

/*    IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);
        CanAttackAgain();
    }*/

    private void AttackRangeCheck()
    {
        float distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;

        if (distance > _monsterStateMachine._monster.stat.AttackRange)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
        /*else
        {
            if (_canAttack)
            {
                StartAnimation(_animData.AttackParameterHash);
                _canAttack = false;
                CoroutineHandler.Start_Coroutine(Test());
            }
        }*/
    }
}
