using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();

    public GameObject Spawn(Define.Object type, string path, int id = -1, Transform parent = null)
    {
        GameObject go = Main.ResourceManager.Instantiate(path, parent);

        switch (type)
        {
            case Define.Object.Player:
                _players.Add(id, go);
                break;
            case Define.Object.Monster:
                //_monsters.Add(id, go);
                break;
        }

        return go;
    }

    public void DeSpawn<T>(Define.Object type, GameObject go)
    {
        switch (type)
        {
            case Define.Object.Player:
                //_players.Add(id, go);
                break;
            case Define.Object.Monster:
                //_monsters.Add(id, go);
                break;
        }
    }
}
