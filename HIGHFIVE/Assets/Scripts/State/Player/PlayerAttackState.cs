using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
