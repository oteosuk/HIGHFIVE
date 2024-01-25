using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Transform spawnZone;     // 몬스터의 스폰존
    public float chaseRadius = 5f;  // 몬스터가 플레이어를 추적하는 범위(몬스터-플레이어)
    public float returnRadius = 8f; // 몬스터가 스폰존으로 돌아가는 범위(몬스터-스폰존)
    public float moveSpeed = 1f;

    public bool isChasing;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    

    void Update()
    {
        MoveProcess();
    }


    void MoveProcess()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToSpawnZone = Vector2.Distance(transform.position, spawnZone.position);

        if (distanceToPlayer < chaseRadius) // 플레이어랑 거리가 좁을때
        {
            if (distanceToSpawnZone > returnRadius)
            {
                isChasing = false;
                ReturnToSpawnZone();
                //2초동안 플레이어 쫓지 못하는상태로직 추가
            }

            isChasing = true;
            ChasePlayer();
        }
        else if (distanceToSpawnZone > 0.1) // 플레이어랑 먼데, 스폰존으로부터 거리가 있다면
        {
            isChasing = false;
            ReturnToSpawnZone();
        }
        else
        {
            animDefault();
        }
    }


    void ChasePlayer()
    {
        // 플레이어를 향해 이동하는 로직을 구현
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
        SetAnimation(angle);
    }


    void ReturnToSpawnZone()
    {
        // 스폰존으로 돌아가는 로직을 구현
        Vector2 direction = (spawnZone.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
        float angle = Mathf.Atan2(spawnZone.position.y - transform.position.y, spawnZone.position.x - transform.position.x) * Mathf.Rad2Deg;
        SetAnimation(angle);
    }


    // 이동 방향에 따라 애니메이션 설정
    void SetAnimation(float angle)
    {
        if (angle > 45 && angle <= 135)
        {
            animMove("isUp");
        }
        else if (angle > 135 && angle <= 225)
        {
            animMove("isLeft");
        }
        else if (angle > 225 && angle <= 315)
        {
            animMove("isDown");
        }
        else if (angle > 315 && angle <= 360 || angle > 0 && angle <= 45)
        {
            animMove("isRight");
        }
    }

    void animMove(string toward)
    {
        if (!anim.GetBool(toward))
        {
            animDefault();
            anim.SetBool(toward, true);
        }
    }

    void animDefault()
    {
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
    }
}
