using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameScene : BaseScene
{
    private Dictionary<string, string> _classMapping = new Dictionary<string, string>
        {
            { "전사", "Warrior" },
            { "도적", "Rogue" },
            { "마법사", "Mage" }
        };
    [SerializeField]
    private GameObject _redCamp;
    [SerializeField]
    private GameObject _blueCamp;
    private CameraController _cameraController;
    private GameObject characterObj;
    protected override void Init()
    {
        base.Init();
        _cameraController = GetComponent<CameraController>();
        _cameraController.characterSpawnEvent += SetInitCameraPosition;
        Vector2 position = Main.GameManager.SelectedCamp == Define.Camp.Red ? _redCamp.transform.position : _blueCamp.transform.position;
        Main.GameManager.CharacterSpawnPos = position;
        string selectClass;

        if (_classMapping.TryGetValue(Main.GameManager.SelectedCharacter, out selectClass))
        {
            characterObj = Main.ResourceManager.Instantiate($"Character/Test", position, syncRequired: true);
            int layer = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Red : (int)Define.Layer.Blue;
            Main.ResourceManager.Instantiate("UI_Prefabs/GameSceneUI");
            Main.GameManager.SpawnedCharacter = characterObj.GetComponent<Character>();
            characterObj.GetComponent<PhotonView>().RPC("SetLayer", RpcTarget.All, layer);
            _cameraController.CallCharacterSpawnEvent();
        }
    }

    private void SetInitCameraPosition()
    {
        Vector3 characterPos = characterObj.transform.position;
        Camera.main.transform.position = new Vector3(characterPos.x, characterPos.y, Camera.main.transform.position.z);
    }
}
