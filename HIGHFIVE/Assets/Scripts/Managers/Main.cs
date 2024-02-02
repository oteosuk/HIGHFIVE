using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main s_instance;

    private GameManager _gameManager = new GameManager();
    private SceneManagerEx _sceneManagerEx = new SceneManagerEx();
    private NetworkManager _networkManager = new NetworkManager();
    private DataManager _dataManager = new DataManager();
    private ResourceManager _resourceManager = new ResourceManager();
    private UIManager _uiManager = new UIManager();
    private ObjectManager _objectManager = new ObjectManager();
    private PoolManager _poolManager = new PoolManager();
    private SoundManager _soundManager = new SoundManager();

    public static ObjectManager ObjectManager { get { return Instance._objectManager; } }
    public static PoolManager PoolManager { get { return Instance._poolManager; } }
    public static GameManager GameManager { get { return Instance._gameManager; } }
    public static ResourceManager ResourceManager { get { return Instance._resourceManager; } }
    public static DataManager DataManager { get { return Instance._dataManager; } }
    public static SceneManagerEx SceneManagerEx { get { return Instance._sceneManagerEx; } }
    public static NetworkManager NetworkManager { get { return Instance._networkManager; } }
    public static UIManager UIManager { get {  return Instance._uiManager; } }
    public static SoundManager SoundManager { get { return Instance._soundManager; } }

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
            s_instance._poolManager.Init();
            s_instance._gameManager.GameInit();
            s_instance._soundManager.Init();
            s_instance._soundManager.PlayBGM("KBF_3m_Town_Castle_01", 1f);
        }
    }
}
