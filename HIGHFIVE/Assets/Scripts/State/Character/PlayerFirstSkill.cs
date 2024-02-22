using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerFirstSkillState : PlayerBaseState
{
    private int _firstSkillHash;
    public PlayerFirstSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_firstSkillHash == 0)
        {
            _firstSkillHash = _playerStateMachine._player.PlayerAnimationData.FirstSkillParameterHash;
        }
    }
    public override void Enter()
    {
        // 기능
        base.Enter();
        _playerStateMachine._player.CharacterSkill.FirstSkill.Execute();

        StartAnimation(_firstSkillHash);
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();

        // 애니메이션 해제
        _playerStateMachine.InputKey = Define.InputKey.None;
        StopAnimation(_firstSkillHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Character myCharacter = _playerStateMachine._player;
        if (myCharacter.Animator.GetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash)) return;
        CheckAndChangeState();
    }

    private void CheckAndChangeState()
    {
        if (_playerStateMachine._player.targetObject != null)
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
        }
        else if (_playerStateMachine.moveInput != (Vector2)_playerStateMachine._player.transform.position)
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
        }
        else
        {
            _playerStateMachine.ChangeState(_playerStateMachine._playerIdleState);
        }
    }
}
