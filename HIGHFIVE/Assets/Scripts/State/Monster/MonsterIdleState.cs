using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _speedModifier = 0;
        StartAnimation(_animData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.IdleParameterHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        bool isPlayerInRange = RangeInPlayer();
        if (isPlayerInRange == true)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
    }

    private bool RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red") | LayerMask.GetMask("Blue"));
        return playerCollider != null ? true : false;
    }
}