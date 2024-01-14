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

    private Button _createRoomBtn;
    private GameObject _roomListContent;
    private GameObject _roomPrefab;
    private List<GameObject> _rooms = new List<GameObject>();
    private float contentHeight = 0f;


    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects),true);

        _createRoomBtn = Get<Button>((int)Buttons.CreateRoomBtn);
        _roomListContent = Get<GameObject>((int)GameObjects.RoomListContent);
        _roomPrefab = Resources.Load("Prefabs/UI_Prefabs/Rooms").GameObject();

        AddUIEvent(_createRoomBtn.gameObject, Define.UIEvent.Click, OnCreateRoomButtonClicked);
    }

    private void OnCreateRoomButtonClicked(PointerEventData pointerEventData)
    {
        Main.NetworkManager.MakeRoom(PhotonNetwork.NickName);
    }

    private void OnEnterRoomClicked(PointerEventData pointerEventData)
    {
        PhotonNetwork.JoinRoom(pointerEventData.pointerClick.transform.Find("RoomName").GetComponent<TMP_Text>().text);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            GameObject newRoom = Instantiate(_roomPrefab, _roomListContent.transform);
            _rooms.Add(newRoom);
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
