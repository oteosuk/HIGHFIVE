using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 프리팹으로 만든 몬스터 프리팹
    //private GameObject[] spawnZones;

    void Start()
    {
        SpawnMonsters();
    }

    void SpawnMonsters()
    {
        // 자신에게 부착된 SpawnZoneGroup 스크립트 가져오기

        Transform[] spawnZones = GetComponentsInChildren<Transform>();

        // 각 SpawnZone에서 몬스터 생성
        foreach (Transform spawnZone in spawnZones)
        {
            SpawnMonsterInZone(spawnZone);
        }
    }

    void SpawnMonsterInZone(Transform spawnZone)
    {
        // 몬스터를 생성할 위치 가져오기
        Vector3 spawnPosition = spawnZone.position;

        // 몬스터 생성
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // 생성된 몬스터를 SpawnZone에 부모로 설정
        monster.transform.parent = spawnZone;
    }
}
