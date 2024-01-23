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

    }

    public virtual void OnMove()
    {

    }

    public virtual void OnDie()
    {

    }
}
