using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine _monsterStateMachine;
    protected Animator _anim;
    protected MonsterAnimationData _animData;
    protected float _speedModifier;

    public MonsterBaseState(MonsterStateMachine monsterStateMachine)
    {
        _monsterStateMachine = monsterStateMachine;
        _anim = _monsterStateMachine._monster.Animator;
        _animData = _monsterStateMachine._monster.MonsterAnimationData;
        _speedModifier = _monsterStateMachine.moveSpeedModifier;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
       
    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void StateUpdate()
    {
        if (_monsterStateMachine._monster.stat.CurHp <= 0)
        {
            OnDie();
        }

        if (_monsterStateMachine.moveSpeedModifier <= 0)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        }
    }

    protected virtual void OnDie()
    {
        _monsterStateMachine.ChangeState(_monsterStateMachine._monsterDieState);
    }

    protected virtual void StartAnimation(int hashValue)
    {
        _monsterStateMachine._monster.Animator.SetBool(hashValue, true);
    }
    protected virtual void StopAnimation(int hashValue)
    {
        _monsterStateMachine._monster.Animator.SetBool(hashValue, false);
    }
}
