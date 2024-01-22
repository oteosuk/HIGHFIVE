using Photon.Pun;
using Photon.Realtime;
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
    private bool _isReady = false;
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
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.LoadLevel((int)Define.Scene.SelectScene);
            }
        }
        else
        {
            GetComponent<PhotonView>().RPC("SetReadyImage", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            SetPlayerReady(!_isReady);
        }
    }

    //플레이어의 레디 상태를 제어하는 함수
    private void SetPlayerReady(bool ready)
    {
        _isReady = ready;

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", _isReady }
        });
    }

    [PunRPC]
    public void SetReadyImage(string userName)
    {
        if (Main.NetworkManager.photonReadyImageDict.TryGetValue(userName, out Image image))
        {
            if (image.color == Define.RedColor) image.color = Define.GreenColor;
            else image.color = Define.RedColor;

            _playerListContent.transform.Find($"{userName}Player").transform.Find("ReadyImage").GetComponent<Image>().color = image.color;
        }
        
    }
}
