using UnityEngine;

public class Main : MonoBehaviour
{
    private static Main s_instance;

    private static Main Instance
    {
        get { MainInit(); return s_instance; }
    }

    //Main에 Manager들 생성
    private UIManager _uiManager = new UIManager();
    private GameManager _gameManager = new GameManager();
    private PoolManager _poolManager = new PoolManager();
    private DataManager _dataManager = new DataManager();
    private SoundManager _soundManager = new SoundManager();
    private ObjectManager _objectManager = new ObjectManager();
    private SceneManagerEx _sceneManagerEx = new SceneManagerEx();
    private NetworkManager _networkManager = new NetworkManager();
    private ResourceManager _resourceManager = new ResourceManager();

    public static UIManager UIManager { get { return Instance._uiManager; } }
    public static GameManager GameManager { get { return Instance._gameManager; } }
    public static PoolManager PoolManager { get { return Instance._poolManager; } }
    public static DataManager DataManager { get { return Instance._dataManager; } }
    public static SoundManager SoundManager { get { return Instance._soundManager; } }
    public static ObjectManager ObjectManager { get { return Instance._objectManager; } }
    public static SceneManagerEx SceneManagerEx { get { return Instance._sceneManagerEx; } }
    public static NetworkManager NetworkManager { get { return Instance._networkManager; } }
    public static ResourceManager ResourceManager { get { return Instance._resourceManager; } }

    

    private void Awake()
    {
        MainInit();
    }

    private static void MainInit()
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
            s_instance.InitializeManagers();
        }
    }

    private void InitializeManagers()
    {
        _poolManager.PoolInit();
        _gameManager.GameInit();
        _soundManager.SoundInit();
    }
}