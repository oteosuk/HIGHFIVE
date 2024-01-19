using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerReadyController : UIBase
{
    private enum Buttons
    {
        ReadyBtn
    }
    private enum GameObjects
    {
        PlayerListContent
    }

    private Button _readyBtn;
    private bool isReady = false;
    private GameObject _playerListContent;
    private void Start()
    {
        Bind<Button>(typeof(Buttons),true);
        Bind<GameObject>(typeof(GameObjects), true);

        _playerListContent = Get<GameObject>((int)GameObjects.PlayerListContent);
        _readyBtn = Get<Button>((int)Buttons.ReadyBtn);

        AddUIEvent(_readyBtn.gameObject, Define.UIEvent.Click, OnStartOrReadyButtonClicked);
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

            if (isAllPlayerReady)
            {
                ExitGames.Client.Photon.Hashtable customProperties = PhotonNetwork.CurrentRoom.CustomProperties;
                
                customProperties["IsGameStarted"] = true;
                Debug.Log(customProperties["IsGameStarted"]);
                PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
                PhotonNetwork.LoadLevel((int)Define.Scene.SelectScene);
            }
        }
        else
        {
            //if (Main.NetworkManager.photonReadyImageDict.TryGetValue(PhotonNetwork.NickName, out Image image))
            //{
                
            //}
            if (isReady == false)
            {
                GetComponent<PhotonView>().RPC("SetReadyImage", RpcTarget.AllBuffered, PhotonNetwork.NickName);
                SetPlayerReady(true);
            }
            else
            {
                GetComponent<PhotonView>().RPC("SetReadyImage", RpcTarget.AllBuffered, PhotonNetwork.NickName);
                SetPlayerReady(false);
            }
        }
    }
    //내 기준에서만 딕셔너리의 색깔을 바꾸고 있었다

    //플레이어의 레디 상태를 제어하는 함수
    private void SetPlayerReady(bool ready)
    {
        isReady = ready;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", isReady }
        });
    }

    [PunRPC]
    public void SetReadyImage(string userName)//id
    {
        //로컬에서 딕셔너리에서 각자 정보를 갖고있음 -> 본인 뿐 아니라 다른 정보도 갖고있게 해줌 -> key로 id -> 본인의 id값을 매개변수
        //매개변수로 넘오온 id 디
        if (Main.NetworkManager.photonReadyImageDict.TryGetValue(userName, out Image image))
        {
            if (image.color == Define.RedColor) image.color = Define.GreenColor;
            else image.color = Define.RedColor;

            _playerListContent.transform.Find($"{userName}Player").transform.Find("ReadyImage").GetComponent<Image>().color = image.color;
        }
        
    }
}
