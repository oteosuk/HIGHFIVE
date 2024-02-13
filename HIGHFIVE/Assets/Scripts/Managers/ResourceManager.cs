using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf("/");
            if (index >= 0)
            {
                name = name.Substring(index+1);
            }

            GameObject gameObject = Main.PoolManager.GetOriginal(name);
            if (gameObject != null) return gameObject as T;
        }
        return Resources.Load<T>(path);
    }
    
    public GameObject Instantiate(string path, Transform parent = null, string changingName = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load Prefab: {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
        {
            return Main.PoolManager.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);
        if (changingName != null) go.name = $"{changingName}";
        else
        {
            go.name = original.name;
        }

        return go;
    }
    public GameObject Instantiate(string path, Vector2 position, Quaternion rotation = default, Transform parent = null, bool syncRequired = false)
    {
        if (syncRequired)
        {
            if (path.Contains("Monster") )
            {
                return InstantiateMonster(path, position, parent);
            }
            else
            {
                return PhotonNetwork.Instantiate($"Prefabs/{path}", position, Quaternion.identity);
            }
        }

        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load Prefab: {path}");
            return null;
        }

        

        if (original.GetComponent<Poolable>() != null && syncRequired)
        {
            //나중에 포톤 + 풀링
        }
        else if (original.GetComponent<Poolable>() != null && !syncRequired)
        {
            return Main.PoolManager.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, position, rotation, parent);
        go.name = original.name;

        return go;
    }

    private GameObject InstantiateMonster(string path, Vector2 position, Transform parent = null)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject monster = PhotonNetwork.Instantiate($"Prefabs/{path}", position, Quaternion.identity);
            monster.transform.SetParent(parent);
            return monster;
        }
        return null;
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            Main.PoolManager.Push(poolable);
            return;
        }

        Object.Destroy(obj);
    }
}
