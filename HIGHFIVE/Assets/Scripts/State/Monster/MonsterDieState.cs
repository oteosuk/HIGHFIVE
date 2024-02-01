using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDieState : MonsterBaseState
{
    public MonsterDieState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }
    public override void Enter()
    {
        StartAnimation(_animData.DieParameterHash);


        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.DieParameterHash);
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
