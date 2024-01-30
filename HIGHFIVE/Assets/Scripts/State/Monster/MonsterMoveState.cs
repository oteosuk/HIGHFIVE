using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonsterMoveState : MonsterBaseState
{
    private bool isReturnToSpawn = false;
    public MonsterMoveState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _monsterStateMachine.moveSpeedModifier = 1;
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Move Exit");
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        _monsterStateMachine.targetObject = RangeInPlayer();
        //_monsterStateMachine.targetObject = GameObject.FindWithTag("Player");
        
        //float spawnToDistance = (_monsterStateMachine._monster._spawnPoint - (Vector2)_monsterStateMachine._monster.transform.position).magnitude;
        if (_monsterStateMachine.targetObject != null)
        {
        float distance = (_monsterStateMachine.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;
            if (distance <= _monsterStateMachine._monster.stat.AttackRange)
            {
                _monsterStateMachine.ChangeState(_monsterStateMachine._monsterAttackState);
            }
            //else if (distance > _monsterStateMachine._monster.stat.SightRange)
            //{
            //    Debug.Log("1");

            //}
            else
            {
                float spawnToDistance = (_monsterStateMachine._monster._spawnPoint - (Vector2)_monsterStateMachine._monster.transform.position).magnitude;
                if (spawnToDistance >= 8f || isReturnToSpawn == true) ReturnToSpawnZone();
                else
                {
                    if(isReturnToSpawn == false)
                        MoveMonsterToTarget();
                }
            }
        }
        else
        {
            ReturnToSpawnZone();
        }

    }
    private GameObject RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red"));
        return playerCollider != null ? playerCollider.gameObject : null;
    }

    private void MoveMonsterToTarget()
    {
        _monsterStateMachine._monster.transform.position = Vector2.MoveTowards(
            _monsterStateMachine._monster.transform.position,
             _monsterStateMachine.targetObject.transform.position,
            _monsterStateMachine._monster.stat.MoveSpeed * Time.deltaTime * _monsterStateMachine.moveSpeedModifier
        );
    }

    // 스폰존으로 돌아가는 로직을 구현
    private void ReturnToSpawnZone()
    {
        isReturnToSpawn = true;
        Vector2 direction = _monsterStateMachine._monster._spawnPoint - (Vector2)_monsterStateMachine._monster.transform.position;
        //_monsterStateMachine._monster.transform.Translate(direction.normalized * Time.deltaTime * _monsterStateMachine.Speed);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _monsterStateMachine._monster.transform.position = Vector2.MoveTowards(
    _monsterStateMachine._monster.transform.position,
     _monsterStateMachine._monster._spawnPoint,
    _monsterStateMachine._monster.stat.MoveSpeed * Time.deltaTime * _monsterStateMachine.moveSpeedModifier);
        SetAnimation(angle);
        if ((Vector2)_monsterStateMachine._monster.transform.position == _monsterStateMachine._monster._spawnPoint)
        {
            isReturnToSpawn = false;
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        }
    }
    void SetAnimation(float angle)
    {
        if (angle >= 45 && angle < 135)
        {
            //Debug.Log("윗방향  :  " + angle);
            animSet("isUp");
        }
        else if (angle >= 135 && angle <= 180 || angle >= -180 && angle < -135)
        {
            //Debug.Log("왼쪽방향  :  " + angle);
            animSet("isLeft");
        }
        else if (angle >= -135 && angle < -45)
        {
            //Debug.Log("아래방향  :  " + angle);
            animSet("isDown");
        }
        else if (angle >= -45 && angle < 0 || angle >= 0 && angle < 45)
        {
            //Debug.Log("오른쪽방향  :  " + angle);
            animSet("isRight");
        }
        else
        {
            Debug.Log("예외 앵글" + angle);
        }
    }
    // 해당 bool 파라미터가 true가 아닐때만 true가 되게끔
    void animSet(string animName)
    {
        if (!_monsterStateMachine._monster.Animator.GetBool(animName))
        {
            animDefault();
            _monsterStateMachine._monster.Animator.SetBool(animName, true);
        }
    }
    // anim bool파라미터 초기화
    void animDefault()
    {
        _monsterStateMachine._monster.Animator.SetBool("isIdle", false);
        _monsterStateMachine._monster.Animator.SetBool("isUp", false);
        _monsterStateMachine._monster.Animator.SetBool("isDown", false);
        _monsterStateMachine._monster.Animator.SetBool("isLeft", false);
        _monsterStateMachine._monster.Animator.SetBool("isRight", false);
    }
}
