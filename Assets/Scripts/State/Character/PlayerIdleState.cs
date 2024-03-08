using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    private int _idleHash;
    public PlayerIdleState(PlayerStateMachine playerStateMachine) :  base(playerStateMachine)
    {
        if (_idleHash == 0)
        {
            _idleHash = _playerStateMachine._player.PlayerAnimationData.IdleParameterHash;
        }
    }

    public override void Enter()
    {
        _playerStateMachine.moveSpeedModifier = 0f;
        if (_playerStateMachine._player.NavMeshAgent.enabled == true)
        {
            _playerStateMachine._player.NavMeshAgent.isStopped = true;
        }

        _playerStateMachine.moveInput = _playerStateMachine._player.transform.position;
        base.Enter();
        StartAnimation(_idleHash);
        // 애니메이션 호출
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_idleHash);
        // 애니메이션 해제
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (Main.GameManager.isAutoAttack)
        {
            AutoAttack();
        }
        if (_playerStateMachine.moveInput != (Vector2)_playerStateMachine._player.transform.position)
        {
            OnMove();
            return;
        }
    }

    protected override void OnMove()
    {
        base.OnMove();
        _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
    }

    private void AutoAttack()
    {
        GameObject searchedObj = SearchObjInSight();
        if (searchedObj != null)
        {
            Character myCharacter = _playerStateMachine._player;
            float distance = (searchedObj.transform.position - myCharacter.transform.position).magnitude;

            if (myCharacter.stat.AttackRange > distance)
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerAttackState);
            }
            else
            {
                _playerStateMachine.ChangeState(_playerStateMachine._playerMoveState);
            }
        }
    }

    private GameObject FindClosestObj(Vector2 origin, Collider2D[] colliders)
    {
        float closestDistance = Mathf.Infinity;
        Collider2D closestCollider = null;

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector2.Distance(origin, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = collider;
            }
        }

        return closestCollider.gameObject;
    }

    private GameObject SearchObjInSight()
    {
        int monsterMask = LayerMask.GetMask("Monster");
        int enemyMask = Main.GameManager.SelectedCamp == Define.Camp.Red ? LayerMask.GetMask("Blue") : LayerMask.GetMask("Red");

        Vector2 playerPos = _playerStateMachine._player.transform.position;
        float playerSightRange = _playerStateMachine._player.stat.SightRange;

        Collider2D[] monsterColliders = Physics2D.OverlapCircleAll(playerPos, playerSightRange, monsterMask);
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(playerPos, playerSightRange, enemyMask);

        if (enemyColliders.Length != 0 || monsterColliders.Length != 0)
        {
            GameObject targetObj = FindClosestObj(playerPos, enemyColliders.Length != 0 ? enemyColliders : monsterColliders);
            _playerStateMachine._player.targetObject = targetObj;
            return targetObj;
        }

        return null;
    }
}