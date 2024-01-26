using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Transform spawnZone;
    private float _chaseRadius = 5f;  // 몬스터가 플레이어를 추적하는 범위(몬스터-플레이어)
    private float _returnRadius = 8f; // 몬스터가 스폰존으로 돌아가는 범위(몬스터-스폰존)
    private float _moveSpeed = 1.5f;

    private Animator _anim;

    public bool isReturn = false;
    void ChaseAgain()
    {
        isReturn = false;
    }

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        float distanceToSpawnZone = Vector2.Distance(transform.position, spawnZone.position);

        MoveProcess(distanceToPlayer, distanceToSpawnZone);

        /*if(!isReturn && distanceToSpawnZone > _returnRadius)
        {
            isReturn = true;
        }*/
    }


    void MoveProcess(float distanceToPlayer, float distanceToSpawnZone)
    {
        /*if (distanceToSpawnZone > _returnRadius) // 스폰존으로부터 너무 멀리갔을때
        {
            if (timeSinceReturn < returnCooldown) // 쿨다운 중이라면
            {
                timeSinceReturn += Time.deltaTime; // 경과 시간 갱신
                ReturnToSpawnZone();
            }
            //ReturnToSpawnZone();
            return;
        }
        else ChasePlayer();*/

        if (distanceToPlayer < _chaseRadius) // 플레이어가 몬스터추적 반경안일때
        {
            if (distanceToSpawnZone > _returnRadius) // 쫓아가다가 스폰존으로부터 너무 멀리갔을때
            { 
                _returnRadius = 0f; // 반경을 0 으로 바꿔서 무조건 돌아가게끔
                _moveSpeed = 3f;
                ReturnToSpawnZone();
                return; // 밑에 ChasePlayer()를 실행시키지 않게 return;
            }
            _moveSpeed = 1.5f;
            ChasePlayer();
        }
        else if (distanceToSpawnZone > 0.1) // 스폰존으로부터 거리가 있다면
        {
            ReturnToSpawnZone();
        }
        else // 스폰존에 도착했다면
        {
            _returnRadius = 8f; // 반경초기화
            animSet("isIdle");
        }
    }


    // 플레이어를 향해 이동하는 로직을 구현
    void ChasePlayer()
    {
        Vector2 direction = player.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * _moveSpeed);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SetAnimation(angle);
    }


    // 스폰존으로 돌아가는 로직을 구현
    void ReturnToSpawnZone()
    {
        Vector2 direction = spawnZone.position - transform.position;
        transform.Translate(direction.normalized * Time.deltaTime * _moveSpeed);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SetAnimation(angle);
    }


    // 이동 방향에 따라 애니메이션 설정
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
        if (!_anim.GetBool(animName))
        {
            animDefault();
            _anim.SetBool(animName, true);
        }
    }


    // anim bool파라미터 초기화
    void animDefault()
    {
        _anim.SetBool("isIdle", false);
        _anim.SetBool("isUp", false);
        _anim.SetBool("isDown", false);
        _anim.SetBool("isLeft", false);
        _anim.SetBool("isRight", false);
    }
}
