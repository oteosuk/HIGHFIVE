using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviourPunCallbacks
{
    //[SerializeField] private GameObject MonsterPrefab;
    private KeyValuePair<Transform, GameObject>[] array = new KeyValuePair<Transform, GameObject>[5];
    private int _respawnDelayTime;
    protected float _curTime;
    protected bool isFull;
    [SerializeField] Transform[] _spawonArray; // 인스펙터에서 포지션 직접 할당해야함

    protected virtual void Start()
    {
        _respawnDelayTime = 3;
        _curTime = 0;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            array[i] = new KeyValuePair<Transform, GameObject>(_spawonArray[i], null);
        }
    }

    protected virtual void Update()
    {

    }

    protected void ReSpawnProcess(string prefabPath)
    {
        CheckFull();
        if (!isFull)
        {
            _curTime -= Time.deltaTime;
            if (_curTime <= 0)
            {
                ReSpawn(prefabPath);
            }
        }
    }

    protected void ReSpawn(string path)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            if (array[i].Value == null)
            {
                Vector3 spawnPosition = array[i].Key.position;
                GameObject mons = Main.ResourceManager.Instantiate(path, spawnPosition, parent: array[i].Key, syncRequired: true);
                array[i] = new KeyValuePair<Transform, GameObject>(array[i].Key, mons);
                _curTime = _respawnDelayTime;
                break;
            }
        }
    }

    protected void CheckFull()
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
