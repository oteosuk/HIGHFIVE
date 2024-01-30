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

    void CanAttackAgain()
    {
        _canAttack = true;
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);
        CanAttackAgain();
    }

    private void OnAttack()
    {
        float distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;

        if (distance > _monsterStateMachine._monster.stat.AttackRange || distance > _monsterStateMachine._monster.stat.SightRange)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
        else
        {
            if (_canAttack)
            {
                StartAnimation(_animData.AttackParameterHash);
                _canAttack = false;
                CoroutineHandler.Start_Coroutine(Test());
            }
        }
    }
}
