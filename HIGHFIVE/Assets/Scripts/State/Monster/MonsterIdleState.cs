using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    private int _idleHash;

    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        if (_idleHash == 0)
        {
            _idleHash = _monsterStateMachine._monster.MonsterAnimationData.IdleParameterHash;
        }
    }
    public override void Enter()
    {
        base.Enter();
        _monsterStateMachine.moveSpeedModifier = 0;
        StartAnimation(_idleHash);
        Debug.Log("Idle  In");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_idleHash);
        Debug.Log("Idle  Exit");
    }
    public override void StateUpdate()
    {
        bool isPlayerInRange = RangeInPlayer();
        Debug.Log(isPlayerInRange);
        if (isPlayerInRange == true)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
            Debug.Log("Move들어가나?");
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
