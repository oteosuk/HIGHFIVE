using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform 컴포넌트
    public float chaseRadius = 5f; // 몬스터가 플레이어를 추적하는 범위
    public float returnRadius = 8f; // 몬스터가 스폰존으로 돌아가는 범위
    public Transform spawnZone; // 몬스터의 스폰존
    public float moveSpeed = 1f;

    public bool isChasing = false;
    public bool isStopped;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToSpawnZone = Vector2.Distance(transform.position, spawnZone.position);

        if (distanceToPlayer < chaseRadius) // 플레이어랑 거리가 좁을때
        {
            if (isChasing && distanceToSpawnZone > returnRadius)
            {
                isStopped = false;
                isChasing = false;
                ReturnToSpawnZone();
            }
            isStopped = false;
            isChasing = true;
            ChasePlayer();
        }
        else if(distanceToSpawnZone > 0.1)
        {
            isStopped = false;
            isChasing = false;
            ReturnToSpawnZone();
        }
        else
        {
            isStopped = true;
        }
    }

    void ChasePlayer()
    {
        // 플레이어를 향해 이동하는 로직을 구현
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }

    void ReturnToSpawnZone()
    {
        // 스폰존으로 돌아가는 로직을 구현
        Vector2 direction = (spawnZone.position - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * moveSpeed);
    }
}
