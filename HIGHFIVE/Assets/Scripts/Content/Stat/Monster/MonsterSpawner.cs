using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    //public GameObject monsterPrefab; // 프리팹으로 만든 몬스터 프리팹
    private int _respawnDelayTime;
    private float _curTime;

    void Start()
    {
        _respawnDelayTime = 3;
        _curTime = 0;
    }

    void Update()
    {
        _curTime -= Time.deltaTime;
        if (_curTime <= 0)
        {
            RealReSpawn();
        }
    }

    void SpawnMonsters()
    {
        // 생성된 몬스터를 SpawnZone에 부모로 설정
        //monster.transform.parent = spawnZone;
    }


    void RealReSpawn()
    {
        Transform childTransfrom = GetComponentInChildren<Transform>();
        Vector3 spawnPosition = childTransfrom.position;
        GameObject mons = Main.ResourceManager.Instantiate($"Monster/Enemy", spawnPosition);
        _curTime = _respawnDelayTime;
    }
}
