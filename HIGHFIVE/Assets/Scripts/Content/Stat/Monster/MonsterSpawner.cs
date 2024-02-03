using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private KeyValuePair<Transform, GameObject>[] array = new KeyValuePair<Transform, GameObject>[5];
    private int _respawnDelayTime;
    private float _curTime;
    private bool isFull;
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
        CheckFull();
        if (!isFull)
        {
            _curTime -= Time.deltaTime;
            if (_curTime <= 0)
            {
                ReSpawn();
            }
        }
    }

    void ReSpawn()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i].Value == null)
            {
                Vector3 spawnPosition = array[i].Key.position;
                GameObject mons = Main.ResourceManager.Instantiate($"Monster/Enemy", spawnPosition, parent: array[i].Key, syncRequired:true);
                array[i] = new KeyValuePair<Transform, GameObject>(array[i].Key, mons);
                _curTime = _respawnDelayTime;
                break;
            }
        }
    }

    private void CheckFull()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i].Value != null)
            {
                isFull = true;
            }
            else
            {
                isFull = false;
                break;
            }
        }
    }
}
