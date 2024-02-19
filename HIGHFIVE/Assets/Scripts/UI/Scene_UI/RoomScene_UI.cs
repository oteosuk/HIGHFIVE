using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomScene_UI : UIBase
{
    private enum Buttons
    {
        BackBtn
    }
    private enum Texts
    {
        ReadyTxt,
        RoomName,
    }
    private enum GameObjects
    {
        PlayerListContent
    }

    private TMP_Text _readyTxt;
    private TMP_Text _roomName;
    private Button _backToLobbyBtn;
    private GameObject _playerListContent;
    private bool isClicked;

    private void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        Bind<TMP_Text>(typeof(Texts), true);
        Bind<GameObject>(typeof(GameObjects), true);

        _roomName = Get<TMP_Text>((int)Texts.RoomName);
        _readyTxt = Get<TMP_Text>((int)Texts.ReadyTxt);
        _backToLobbyBtn = Get<Button>((int)Buttons.BackBtn);
        _playerListContent = Get<GameObject>((int)GameObjects.PlayerListContent);
        isClicked = false;

        if (PhotonNetwork.IsMasterClient) _readyTxt.text = "GameStart";
        _roomName.text = $"{PhotonNetwork.CurrentRoom.Name}";

        AddUIEvent(_backToLobbyBtn.gameObject, Define.UIEvent.Click, OnBackToLobbyButtonClicked);
    }



    //로비 버튼 클릭 했을 때 호출 되는 함수
    private void OnBackToLobbyButtonClicked(PointerEventData pointerEventData)
    {
        if (isClicked) { return; }
        isClicked = true;
        Player[] currentRoomPlayer = PhotonNetwork.PlayerList;
        foreach (Player player in currentRoomPlayer)
        {
            Main.NetworkManager.photonPlayerDict[player.NickName] = null;
            Main.NetworkManager.photonReadyImageDict.Remove(player.NickName);
        }

        Main.NetworkManager.photonRoomDict[PhotonNetwork.CurrentRoom.Name] = false;
        

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", false }
        });
        if (!PhotonNetwork.LeaveRoom()) { isClicked = false; }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Transform masterPlayer = _playerListContent.transform.Find($"{newMasterClient.NickName}Player");
        TMP_Text playerRole = masterPlayer.transform.Find("PlayerRole").GetComponent<TMP_Text>();
        Image playerReadyImage = masterPlayer.transform.Find("ReadyImage").GetComponent<Image>();
        playerReadyImage.gameObject.SetActive(false);
        playerRole.text = "[방장]";
        if (PhotonNetwork.IsMasterClient) _readyTxt.text = "GameStart";
    }

}
