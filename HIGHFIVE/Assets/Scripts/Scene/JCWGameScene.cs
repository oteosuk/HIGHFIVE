using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JCWGameScene : BaseScene
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
    protected override void Init()
    {
        base.Init();
        Vector2 position = Main.GameManager.SelectedCamp == Define.Camp.Red ? _redCamp.transform.position : _blueCamp.transform.position;
        string selectClass;
        GameObject characterObj;
        if (_classMapping.TryGetValue(Main.GameManager.SelectedCharacter, out  selectClass))
        {
            characterObj = Main.ObjectManager.Spawn($"Character/{selectClass}", position, syncRequired:true);

            
            characterObj.layer = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Red : (int)Define.Layer.Blue;
            //characterObj.GetComponent<PhotonView>().RPC("SetLayer", RpcTarget.AllBuffered, characterObj.layer);
            Main.GameManager.SpawnObj = characterObj.GetComponent<Character>();
        } 
    }

    //[PunRPC]
    //public void SetLayer(int layer)
    //{
    //    if (Main.NetworkManager.photonPlayerDict.TryGetValue(PhotonNetwork.NickName, out Player myPlayer))
    //    {
    //        if (myPlayer.CustomProperties.TryGetValue("layer", out int lay))
    //        {
    //            layer = lay;
    //        }
    //    }
    //}
}
