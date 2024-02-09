using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerBaseState
{

    public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        _playerStateMachine.moveSpeedModifier = 0f;
        _playerStateMachine._player.GetComponent<PhotonView>().RPC("SetLayer", RpcTarget.All, (int)Define.Layer.Default);
        _playerStateMachine._player.Collider.isTrigger = true;
        if (Main.GameManager.page != Define.Page.Battle)
        {
            _playerStateMachine._player.Revival(5);
        }
        StartAnimation(_playerStateMachine._player.PlayerAnimationData.DieParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        // 애니메이션 해제
        StopAnimation(_playerStateMachine._player.PlayerAnimationData.DieParameterHash);
    }

    public override void StateUpdate()
    {

    }
}
