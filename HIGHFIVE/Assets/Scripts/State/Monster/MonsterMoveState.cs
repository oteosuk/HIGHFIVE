using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : MonsterBaseState
{
    public MonsterMoveState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (_monsterStateMachine._monster.Rigidbody.velocity != Vector2.zero)
        {
            _monsterStateMachine._monster.Rigidbody.velocity = Vector2.zero;
            Debug.Log("Move In");
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Move Exit");
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        Move();
    }

    private void Move()
    {
        Vector2 playerPos = RangeInPlayer();
        Vector2 currentPos = _monsterStateMachine._monster.transform.position;

        Debug.Log("Move에서 player 좌표 : " + playerPos);
        Debug.Log("Move에서 monster 좌표 : " + currentPos);
        float distanceToPlayer = Vector2.Distance(currentPos, playerPos);
        float distanceToSpawnZone = Vector2.Distance(currentPos, _monsterStateMachine._monster._spawnPoint);
        Debug.Log("스폰존 : " + distanceToSpawnZone);
        Debug.Log("플레이어존 : " + distanceToPlayer);
        if(distanceToPlayer < _monsterStateMachine._monster.stat.SightRange)
        {
            if(distanceToSpawnZone > 8f)
            {
                Debug.Log("들어와졌는지 체크 distacneToSpawnZone");
                //스폰존으로 돌아가는 로직?
                Vector2 spawnDirection = (_monsterStateMachine._monster._spawnPoint - currentPos).normalized;
                _monsterStateMachine._monster.Rigidbody.velocity = spawnDirection * _monsterStateMachine._monster.stat.MoveSpeed;
                Debug.Log("Velocity: " + _monsterStateMachine._monster.Rigidbody.velocity);

                //float spawnToAngle = Mathf.Atan2(_monsterStateMachine._monster._spawnPoint.y - currentPos.y, _monsterStateMachine._monster._spawnPoint.x-currentPos.x) * Mathf.Rad2Deg;
            }
            // 플레이어를 향해 이동하는 로직?
            Debug.Log("플레이어 이동로직 체크");
            Vector2 playerDirection = (playerPos - currentPos).normalized;
            _monsterStateMachine._monster.Rigidbody.velocity = playerDirection  * _monsterStateMachine._monster.stat.MoveSpeed;
            Debug.Log("Velocity: " + _monsterStateMachine._monster.Rigidbody.velocity);

            //float playerToAngle = Mathf.Atan2(playerPos.y - currentPos.y, playerPos.x - currentPos.x) * Mathf.Rad2Deg;
        }
        else if(distanceToSpawnZone > 0.1)
        {
            Vector2 spawnDirection = (_monsterStateMachine._monster._spawnPoint - currentPos).normalized;
            _monsterStateMachine._monster.Rigidbody.velocity = spawnDirection * _monsterStateMachine._monster.stat.MoveSpeed;
            //float spawnToAngle = Mathf.Atan2(_monsterStateMachine._monster._spawnPoint.y - currentPos.y, _monsterStateMachine._monster._spawnPoint.x-currentPos.x) * Mathf.Rad2Deg;
        }
        else
        {
            _monsterStateMachine._monster.Rigidbody.velocity = Vector2.zero;
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        }
    }
    protected Vector2 RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red"));
        Debug.Log(playerCollider);
        Debug.Log(LayerMask.GetMask("Red"));
        return playerCollider != null ? playerCollider.transform.position : Vector2.zero;
    }
}
