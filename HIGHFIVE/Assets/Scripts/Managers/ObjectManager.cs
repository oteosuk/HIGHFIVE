using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{

    public GameObject Spawn(string path, Vector2 position, Transform parent = null, bool syncRequired = false)
    {
        GameObject go;

        if (syncRequired)
        {
            go = PhotonNetwork.Instantiate($"Prefabs/{path}", position, Quaternion.identity);
            Debug.Log(go.name);
        }
        else
        {
            go = Main.ResourceManager.Instantiate(path, parent);
            go.transform.position = position;
        }

        return go;
    }

    public void DeSpawn<T>(Define.Object type, GameObject go)
    {
        switch (type)
        {
            case Define.Object.Character:
                //_players.Add(id, go);
                break;
            case Define.Object.Monster:
                //_monsters.Add(id, go);
                break;
        }
    }
}
