using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PoolManager
{
    public class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Queue<Poolable> _poolQueue = new Queue<Poolable>();

        public void Init(GameObject original, int count = 2)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        public Poolable Create()
        {
            Debug.Log(Original);
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null) return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            _poolQueue.Enqueue(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolQueue.Count > 0)
            {
                poolable = _poolQueue.Dequeue();
            }
            else
            {
                poolable = Create();
            }

            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parent;

            return poolable;
        }
    }

    Dictionary<string, Pool> _poolDict = new Dictionary<string, Pool>();
    GameObject _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" };
        }

        Object.DontDestroyOnLoad(_root);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (!_poolDict.ContainsKey(name))
        {
            Object.Destroy(poolable.gameObject);
            return;
        }

        _poolDict[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (!_poolDict.ContainsKey(original.name))
        {
            CreatePool(original);
        }
        return _poolDict[original.name].Pop(parent);
    }

    public void CreatePool(GameObject original, int count = 2)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root.transform;

        _poolDict.Add(original.name, pool);
    }

    public GameObject GetOriginal(string name)
    {
        if (!_poolDict.ContainsKey(name))
        {
            return null;
        }

        return _poolDict[name].Original;
    }
}
