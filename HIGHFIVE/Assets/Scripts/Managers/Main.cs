using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main s_instance;

    private SceneManagerEx _sceneManagerEx = new SceneManagerEx();
    public static SceneManagerEx SceneManagerEx { get { return Instance._sceneManagerEx; } }

    private static Main Instance
    {
        get { Init(); return s_instance; }
    }

    private void Start()
    {
        Init();
    }
  
    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find("Managers");
            if (gameObject == null)
            {
                gameObject = new GameObject("Managers");
                gameObject.AddComponent<Main>();
            }
            DontDestroyOnLoad(gameObject);

            s_instance = gameObject.GetComponent<Main>();
        }
    }
}
