using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public class GameScene : BaseScene
{
    private enum CursorType
    {
        None,
        Nomal,
        Attack
    }

    [SerializeField] GameObject _redCamp;
    [SerializeField] GameObject _blueCamp;
    [SerializeField] GameObject _keyInfoPanel;
    private CameraController _cameraController;
    private GameObject characterObj;
    private Character _character;
    private Texture2D _attackTexture;
    private Texture2D _normalTexture;
    private CursorType _cursorType = CursorType.None;
    private Dictionary<string, string> _classMapping = new Dictionary<string, string>
        {
            { "전사", "Warrior" },
            { "도적", "Rogue" },
            { "마법사", "Mage" }
        };

    private void Update()
    {
        UpdateMouseCursor();
    }
    protected override void Init()
    {
        base.Init();
        _attackTexture = Main.ResourceManager.Load<Texture2D>("Sprites/Cursor/Attack");
        _normalTexture = Main.ResourceManager.Load<Texture2D>("Sprites/Cursor/Normal");
        Main.SceneManagerEx.CurrentScene = Define.Scene.GameScene;
        _cameraController = GetComponent<CameraController>();
        _cameraController.characterSpawnEvent += SetInitCameraPosition;
        Vector2 position = Main.GameManager.SelectedCamp == Define.Camp.Red ? _redCamp.transform.position : _blueCamp.transform.position;
        Main.GameManager.CharacterSpawnPos = position;
        string selectClass;

        Main.UIManager.OpenPopup(_keyInfoPanel);

        if (_classMapping.TryGetValue(Main.GameManager.SelectedCharacter, out selectClass))
        {
            characterObj = Main.ResourceManager.Instantiate($"Character/{selectClass}", position, syncRequired: true);
            _character = characterObj.GetComponent<Character>();
            PhotonView pv = characterObj.GetComponent<PhotonView>();
            pv.RPC("AddPlayer", RpcTarget.AllBuffered, pv.ViewID);
            Main.ResourceManager.Instantiate("UI_Prefabs/GameSceneUI");
            Main.GameManager.SpawnedCharacter = characterObj.GetComponent<Character>();
            _cameraController.CallCharacterSpawnEvent();
            int layer = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Red : (int)Define.Layer.Blue;
            characterObj.layer = layer;
            Main.GameManager.SpawnedCharacter.GetComponent<PhotonView>().RPC("SetLayer", RpcTarget.Others, layer);
        }

        Main.SoundManager.PlayBGM("Battle_Boss_07");
    }

    private void SetInitCameraPosition()
    {
        Vector3 characterPos = characterObj.transform.position;
        Camera.main.transform.position = new Vector3(characterPos.x, characterPos.y, Camera.main.transform.position.z);
    }

    private void UpdateMouseCursor()
    {
        Vector2 mousePoint = _character.MousePoint;
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 30.0f, mask);

        if (hit.collider?.gameObject != null || (_character._playerStateMachine != null && _character._playerStateMachine.isAttackReady))
        {
            if (_cursorType != CursorType.Attack)
            {
                Cursor.SetCursor(_attackTexture, new Vector2(_attackTexture.width / 5, _attackTexture.height / 10), CursorMode.Auto);
                _cursorType = CursorType.Attack;
            }
        }
        else
        {
            if (_cursorType != CursorType.Nomal)
            {
                Cursor.SetCursor(_normalTexture, new Vector2(_normalTexture.width / 5, _normalTexture.height / 10), CursorMode.Auto);
                _cursorType = CursorType.Nomal;
            }
        }
    }
}
