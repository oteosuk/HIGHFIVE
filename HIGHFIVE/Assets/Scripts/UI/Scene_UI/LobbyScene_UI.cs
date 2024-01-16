using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LobbyScene_UI : UIBase
{
    private enum Buttons
    {
        CreateRoomBtn,
        RefreshBtn,
        RecognizeBtn
    }

    private enum GameObjects
    {
        RoomListContent,
        AlertBlock
    }
    private enum Texts
    {
        LobbyInfoTxt
    }
    

    private Button _createRoomBtn;//방 생성 버튼
    private Button _refreshBtn;
    private Button _recognizeBtn;
    private GameObject _roomListContent;//방 리스트 컨텐트
    private GameObject _alertBlock;
    private TMP_Text _lobbyInfoTxt;
    private float contentHeight = 0f;//컨테트의 크기 제어 변수


    private void Start()
    {
        Bind<Button>(typeof(Buttons),true);
        Bind<GameObject>(typeof(GameObjects),true);
        Bind<TMP_Text>(typeof(Texts), true);

        _createRoomBtn = Get<Button>((int)Buttons.CreateRoomBtn);
        _refreshBtn = Get<Button>((int)Buttons.RefreshBtn);
        _recognizeBtn = Get<Button>((int)Buttons.RecognizeBtn);
        _roomListContent = Get<GameObject>((int)GameObjects.RoomListContent);
        _alertBlock = Get<GameObject>((int)GameObjects.AlertBlock);
        _lobbyInfoTxt = Get<TMP_Text>((int)Texts.LobbyInfoTxt);


        AddUIEvent(_createRoomBtn.gameObject, Define.UIEvent.Click, OnCreateRoomButtonClicked);
        AddUIEvent(_refreshBtn.gameObject, Define.UIEvent.Click, OnRefreshButtonClicked);
        AddUIEvent(_recognizeBtn.gameObject, Define.UIEvent.Click, OnRecognizeBtnClicked);
    }

    //CreateRoom버튼을 클릭했을때 호출 되는 함수
    private void OnCreateRoomButtonClicked(PointerEventData pointerEventData)
    {
        Main.NetworkManager.MakeRoom(PhotonNetwork.NickName);
    }

    private void OnRefreshButtonClicked(PointerEventData pointerEventData)
    {
        Main.NetworkManager.photonRoomDict.Clear();
    }

    private void OnRecognizeBtnClicked(PointerEventData pointerEventData)
    {
        Main.UIManager.CloseCurrentPopup(_alertBlock);
    }


    //방 리스트 패널에서 특정 방을 클릭 했을때 호출 되는 함수
    private void OnEnterRoomClicked(PointerEventData pointerEventData)
    {
        //해당 방의 제목으로 방을 찾아서 join
        if (PhotonNetwork.JoinRoom(pointerEventData.pointerClick.transform.Find("RoomName").GetComponent<TMP_Text>().text))
        {
            Main.NetworkManager.photonRoomDict.Clear();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Main.UIManager.OpenPopup(_alertBlock);
    }

    //로비에 있을 때 제3자가 방을 생성하면 로비에 해당 방이 반영이 되도록 해주는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (_lobbyInfoTxt != null) _lobbyInfoTxt.text = $"접속인원: {PhotonNetwork.CountOfPlayers}명";

        foreach (RoomInfo room in roomList)
        {
            //방폭된 방 생성 방지
            if (room.RemovedFromList) continue;
            //내 로컬상에 이미 해당 방이 존재한다면 생성 금지
            if (Main.NetworkManager.photonRoomDict.TryGetValue(room.Name, out bool isContain))
            {
                if (isContain) continue;
            }

            GameObject newRoom = Main.ResourceManager.Instantiate("UI_Prefabs/Rooms", _roomListContent.transform);
            AddUIEvent(newRoom, Define.UIEvent.Click, OnEnterRoomClicked);
            TMP_Text[] newRoomInfoTxts = newRoom.GetComponentsInChildren<TMP_Text>();
            newRoomInfoTxts[0].text = room.Name;
            newRoomInfoTxts[1].text = $"{room.PlayerCount} / {room.MaxPlayers}";
            contentHeight += newRoom.GetComponent<RectTransform>().rect.height;

            Main.NetworkManager.photonRoomDict[room.Name] = true;
        }

        if (contentHeight < _roomListContent.GetComponent<RectTransform>().sizeDelta.y)
        {
            contentHeight = _roomListContent.GetComponent<RectTransform>().sizeDelta.y;
        }

        _roomListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(_roomListContent.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
    }
}
