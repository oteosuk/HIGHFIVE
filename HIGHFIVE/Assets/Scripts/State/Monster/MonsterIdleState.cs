using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    private Animator _anim;
    private MonsterAnimationData _animData;
    private float _speedModifier;

    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        _anim = _monsterStateMachine._monster.Animator;
        _animData = _monsterStateMachine._monster.MonsterAnimationData;
        _speedModifier = _monsterStateMachine.moveSpeedModifier;
    }

    public override void Enter()
    {
        base.Enter();
        _speedModifier = 0;
        StartAnimation(_animData.IdleParameterHash);
        Debug.Log("Idle  In");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.IdleParameterHash);
        Debug.Log("Idle  Exit");
    }

    public override void StateUpdate()
    {
        bool isPlayerInRange = RangeInPlayer();
        Debug.Log(isPlayerInRange);
        if (isPlayerInRange == true)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
            Debug.Log("Move로 ChanageState");
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
    }
    protected override void OnDie()
    {
        base.OnDie();
    }

    private bool RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red"));
        return playerCollider != null ? true : false;
    }
}
