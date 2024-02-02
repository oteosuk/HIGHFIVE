using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private KeyValuePair<Transform, GameObject>[] array = new KeyValuePair<Transform, GameObject>[5];
    private int _respawnDelayTime;
    private float _curTime;
    [SerializeField] Transform[] _spawonArray;

    void Start()
    {
        _respawnDelayTime = 3;
        _curTime = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            array[i] = new KeyValuePair<Transform, GameObject>(_spawonArray[i], null);
        }
    }

    void Update()
    {
        _curTime -= Time.deltaTime;
        if (_curTime <= 0)
        {
            RealReSpawn();
        }
    }

    void RealReSpawn()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i].Value == null)
            {
                Vector3 spawnPosition = array[i].Key.position;
                GameObject mons = Main.ResourceManager.Instantiate($"Monster/Enemy", spawnPosition, parent: array[i].Key);
                array[i] = new KeyValuePair<Transform, GameObject>(array[i].Key, mons);
                _curTime = _respawnDelayTime;
                break;
            }
        }
    }
}
