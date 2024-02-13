using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // Player에 상태들을 관리해주기 위한 클래스
    public Character _player { get; }

    public PlayerIdleState _playerIdleState { get; }
    public PlayerMoveState _playerMoveState { get; }
    public PlayerAttackState _playerAttackState { get; }
    public PlayerDieState _playerDieState { get; }
    public PlayerFirstSkillState PlayerFirstSkillState { get; }

    public Vector2 moveInput { get; set; }
    public float moveSpeedModifier = 1.0f;
    public bool isAttackReady = false;
    public bool isLeftClicked = false;

    public PlayerStateMachine(Character player)
    {
        this._player = player;
        moveInput = _player.transform.position;
        _playerIdleState = new PlayerIdleState(this);
        _playerMoveState = new PlayerMoveState(this);
        _playerAttackState = new PlayerAttackState(this);
        _playerDieState = new PlayerDieState(this);
        PlayerFirstSkillState = new PlayerFirstSkillState(this);
    }

}
