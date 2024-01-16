using Photon.Pun;
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
        BackBtn,
        ReadyBtn
    }
    private enum Texts
    {
        ReadyTxt,
        RoomName,
    }


    private TMP_Text _readyTxt;
    private Button _readyBtn;
    private Button _backToLobbyBtn;
    private bool isReady = false;
    private void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        Bind<TMP_Text>(typeof(Texts), true);


        Get<TMP_Text>((int)Texts.RoomName).text = $"{PhotonNetwork.CurrentRoom.Name} 님의 방";
        _readyTxt = Get<TMP_Text>((int)Texts.ReadyTxt);
        _readyBtn = Get<Button>((int)Buttons.ReadyBtn);
        _backToLobbyBtn = Get<Button>((int)Buttons.BackBtn);


        if (PhotonNetwork.IsMasterClient) _readyTxt.text = "GameStart";

        AddUIEvent(_readyBtn.gameObject, Define.UIEvent.Click, OnStartOrReadyButtonClicked);
        AddUIEvent(_backToLobbyBtn.gameObject, Define.UIEvent.Click, OnBackToLobbyButtonClicked);
    }

    //스타트, 레디 버튼을 클릭 했을때 호출 되는 함수
    private void OnStartOrReadyButtonClicked(PointerEventData pointerEventData)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Player[] players = PhotonNetwork.PlayerList;
            bool isAllPlayerReady = true;
            foreach (Player player in players)
            {
                if (player.IsMasterClient) continue;
                player.CustomProperties.TryGetValue("IsReady", out object value);
                if (value == null || (bool)value == false) isAllPlayerReady = false;
            }

            if (isAllPlayerReady) PhotonNetwork.LoadLevel((int)Define.Scene.SelectScene);
        }
        else
        {
            if (isReady == false)
            {
                //_readyImage.color = Define.GreenColor;
                SetPlayerReady(true);
            }
            else
            {
                //_readyImage.color = Define.RedColor;
                SetPlayerReady(false);
            }
        }
    }
    //로비 버튼 클릭 했을 때 호출 되는 함수
    private void OnBackToLobbyButtonClicked(PointerEventData pointerEventData)
    {
        Player[] currentRoomPlayer = PhotonNetwork.PlayerList;
        foreach (Player player in currentRoomPlayer)
        {
            Main.NetworkManager.photonPlayerDict[player.NickName] = false;
        }

        Main.NetworkManager.photonRoomDict[PhotonNetwork.CurrentRoom.Name] = false;


        PhotonNetwork.LeaveRoom();
    }

    //플레이어의 레디 상태를 제어하는 함수
    private void SetPlayerReady(bool ready)
    {
        isReady = ready;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", isReady }
        });
    }
}
