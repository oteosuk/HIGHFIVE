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
    //private GoogleSheetManager _googleSheetManager = new GoogleSheetManager();

    public static GameManager GameManager { get { return Instance._gameManager; } }
    public static ResourceManager ResourceManager { get { return Instance._resourceManager; } }
    public static DataManager DataManager { get { return Instance._dataManager; } }
    public static SceneManagerEx SceneManagerEx { get { return Instance._sceneManagerEx; } }
    public static NetworkManager NetworkManager { get { return Instance._networkManager; } } // set안써주면 알아서 private(아예 접근이 안됌)
    public static UIManager UIManager { get {  return Instance._uiManager; } }
    //public static GoogleSheetManager GoogleSheetManager { get { return Instance._googleSheetManager; } }

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
