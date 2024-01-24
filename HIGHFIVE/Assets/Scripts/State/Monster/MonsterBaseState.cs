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
        Vector2 playerPos = RangeInPlayer();
        if (playerPos != Vector2.zero)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
        else
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        }
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
        if(_monsterStateMachine._monster.stat.CurHp <= 0)
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

    protected Vector2 RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.AttackRange, LayerMask.GetMask("Player"));  

        return playerCollider != null ? playerCollider.transform.position : Vector2.zero;
    }
}
