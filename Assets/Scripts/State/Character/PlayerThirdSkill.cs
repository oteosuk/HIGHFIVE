using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdSkill : PlayerBaseState
{
    private int _thirdSkillHash;
    public PlayerThirdSkill(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_thirdSkillHash == 0)
        {
            _thirdSkillHash = _playerStateMachine._player.PlayerAnimationData.ThirdSkillParameterHash;
        }
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine._player.NavMeshAgent.isStopped = true;
        _playerStateMachine._player.CharacterSkill.ThirdSkill.Execute();
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        //Character myCharacter = _playerStateMachine._player;
        //if (myCharacter.Animator.GetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash)) return;
    }

    //private void CheckAndChangeState()
    //{
    //    if (_playerStateMachine._player.targetObject != null)
    //    {
    //        _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
    //    }
    //    else if (_playerStateMachine.moveInput != (Vector2)_playerStateMachine._player.transform.position)
    //    {
    //        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    //    }
    //    else
    //    {
    //        _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
    //    }
    //}
}
