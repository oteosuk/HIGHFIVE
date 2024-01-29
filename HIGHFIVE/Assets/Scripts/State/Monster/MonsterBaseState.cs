using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine _monsterStateMachine;

    public MonsterBaseState(MonsterStateMachine monsterStateMachine)
    {
        this._monsterStateMachine = monsterStateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
       
    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void StateUpdate()
    {
        Debug.Log("감지범위 : " + _monsterStateMachine._monster.stat.SightRange);
        Debug.Log("이동속도 : " + _monsterStateMachine._monster.stat.MoveSpeed);
        Debug.Log("체력 : " + _monsterStateMachine._monster.stat.CurHp);
        if (_monsterStateMachine._monster.stat.CurHp <= 0)
        {
            OnDie();
        }
    }

    protected virtual void OnMove()
    {

    }

    protected virtual void OnDie()
    {
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterDieState);
    }

    protected virtual void StartAnimation(int hashValue)
    {
        _monsterStateMachine._monster.Animator.SetBool(hashValue,true);
    }
    protected virtual void StopAnimation(int hashValue)
    {
        _monsterStateMachine._monster.Animator.SetBool(hashValue, false);
    }
}
