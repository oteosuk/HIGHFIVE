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
        RoomInfo
    }

    private TMP_Text _roomInfo;
    private TMP_Text _readyTxt;
    private Image _readyImage;
    private Button _readyBtn;
    private bool isReady = false;
    private void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        Bind<TMP_Text>(typeof(Texts), true);
        
        Get<TMP_Text>((int)Texts.RoomName).text = $"{PhotonNetwork.CurrentRoom.Name} 님의 방";
        _roomInfo = Get<TMP_Text>((int)Texts.RoomInfo);
        _readyTxt = Get<TMP_Text>((int)Texts.ReadyTxt);
        _readyBtn = Get<Button>((int)Buttons.ReadyBtn);

        if (PhotonNetwork.IsMasterClient)
        {
            _readyTxt.text = "GameStart";
        }

        UpdateCurrentRoomInfo();
        AddUIEvent(_readyBtn.gameObject, Define.UIEvent.Click, OnStartOrReadyButtonClicked);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        UpdateCurrentRoomInfo();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName);
        UpdateCurrentRoomInfo();
    }

    private void OnStartOrReadyButtonClicked(PointerEventData pointerEventData)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //모든 플레이어가 준비가 완료됐는지 체크
            //ok면 SelectScene으로 이동 
            PhotonNetwork.LoadLevel((int)Define.Scene.SelectScene);
        }
        else
        {
            if (_readyImage.color == Define.RedColor)
            {
                _readyImage.color = Define.GreenColor;
                SetPlayerReady(true);
            }
            else
            {
                _readyImage.color = Define.RedColor;
                SetPlayerReady(false);
            }
        }
    }

    private void SetPlayerReady(bool ready)
    {
        if (photonView.IsMine)
        {
            isReady = ready;

            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
            {
                { "IsReady", isReady }
            });

            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties);

            photonView.RPC("SyncReadyStatus", RpcTarget.Others, isReady);
        }
    }

    [PunRPC]
    private void SyncReadyStatus(bool ready)
    {
        isReady = ready;
    }

    private void UpdateCurrentRoomInfo()
    {
        _roomInfo.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";
    }
}
