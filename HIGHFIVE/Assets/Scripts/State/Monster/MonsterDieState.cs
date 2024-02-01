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
        PhotonView pv = _monsterStateMachine._monster.GetComponent<PhotonView>();
        StartAnimation(_animData.DieParameterHash);

        //SoundManager.instance.PlayEffect("효과음", 1f);

        /*//5초뒤에 아래 함수 실행
        if (pv.IsMine)
        {
            //pv.RPC();
            PhotonNetwork.Destroy(pv.gameObject);
        }*/

        //GameObject.Destroy(_monsterStateMachine._monster);

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
