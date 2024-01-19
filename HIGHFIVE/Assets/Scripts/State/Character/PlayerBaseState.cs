using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected StateMachine _stateMachine;
    private PlayerStateMachine playerStateMachine;

    public PlayerBaseState(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
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

    public virtual void physicsUpdate()
    {
       
    }

    public virtual void StateUpdate()
    {
        
    }
}
