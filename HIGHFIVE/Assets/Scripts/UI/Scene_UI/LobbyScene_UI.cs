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
    }

    private enum GameObjects
    {
        RoomListContent,
    }

    private Button _createRoomBtn;//방 생성 버튼
    private GameObject _roomListContent;//방 리스트 컨텐트
    //private List<GameObject> _rooms = new List<GameObject>();
    private float contentHeight = 0f;//컨테트의 크기 제어 변수


    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects),true);

        _createRoomBtn = Get<Button>((int)Buttons.CreateRoomBtn);
        _roomListContent = Get<GameObject>((int)GameObjects.RoomListContent);

        AddUIEvent(_createRoomBtn.gameObject, Define.UIEvent.Click, OnCreateRoomButtonClicked);
    }

    //CreateRoom버튼을 클릭했을때 호출 되는 함수
    private void OnCreateRoomButtonClicked(PointerEventData pointerEventData)
    {
        Main.NetworkManager.MakeRoom(PhotonNetwork.NickName);
    }


    //방 리스트 패널에서 특정 방을 클릭 했을때 호출 되는 함수
    private void OnEnterRoomClicked(PointerEventData pointerEventData)
    {
        //해당 방의 제목으로 방을 찾아서 join
        PhotonNetwork.JoinRoom(pointerEventData.pointerClick.transform.Find("RoomName").GetComponent<TMP_Text>().text);
    }

    //로비에 있을 때 제3자가 방을 생성하면 로비에 해당 방이 반영이 되도록 해주는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            if (room.RemovedFromList) continue;
            

            GameObject newRoom = Main.ResourceManager.Instantiate("UI_Prefabs/Rooms", _roomListContent.transform);
            //_rooms.Add(newRoom);
            AddUIEvent(newRoom, Define.UIEvent.Click, OnEnterRoomClicked);
            TMP_Text[] newRoomInfoTxts = newRoom.GetComponentsInChildren<TMP_Text>();
            newRoomInfoTxts[0].text = room.Name;
            newRoomInfoTxts[1].text = $"{room.PlayerCount} / {room.MaxPlayers}";
            contentHeight += newRoom.GetComponent<RectTransform>().rect.height;
        }

        if (contentHeight < _roomListContent.GetComponent<RectTransform>().sizeDelta.y)
        {
            contentHeight = _roomListContent.GetComponent<RectTransform>().sizeDelta.y;
        }

        _roomListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(_roomListContent.GetComponent<RectTransform>().sizeDelta.x, contentHeight);
    }
}
