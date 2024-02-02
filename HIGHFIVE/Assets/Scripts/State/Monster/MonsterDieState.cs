using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonsterDieState : MonsterBaseState
{
    public MonsterDieState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }
    public override void Enter()
    {
        StartAnimation(_animData.DieParameterHash);
        PhotonView pv = _monsterStateMachine._monster.GetComponent<PhotonView>();
        

            _monsterStateMachine._monster.GetComponent<PhotonView>().RPC("RPCDetroy", RpcTarget.All);
        
        
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
