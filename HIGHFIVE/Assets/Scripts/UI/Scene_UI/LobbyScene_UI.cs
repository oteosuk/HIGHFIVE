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
        RecognizeBtn
    }

    private enum GameObjects
    {
        RoomListContent,
        SetRoomBlock
    }
    private enum Texts
    {
        LobbyInfoTxt
    }
    

    private Button _createRoomBtn;//방 생성 버튼
    private GameObject _roomListContent;//방 리스트 컨텐트
    private GameObject _setRoomBlock;
    private TMP_Text _lobbyInfoTxt;
    private float contentHeight = 0f;//컨테트의 크기 제어 변수


    private void Start()
    {
        Bind<Button>(typeof(Buttons),true);
        Bind<GameObject>(typeof(GameObjects),true);
        Bind<TMP_Text>(typeof(Texts), true);

        _createRoomBtn = Get<Button>((int)Buttons.CreateRoomBtn);
        _roomListContent = Get<GameObject>((int)GameObjects.RoomListContent);
        _lobbyInfoTxt = Get<TMP_Text>((int)Texts.LobbyInfoTxt);
        _setRoomBlock = Get<GameObject>((int)GameObjects.SetRoomBlock);


        AddUIEvent(_createRoomBtn.gameObject, Define.UIEvent.Click, OnCreateRoomButtonClicked);
    }

    //CreateRoom버튼을 클릭했을때 호출 되는 함수
    private void OnCreateRoomButtonClicked(PointerEventData pointerEventData)
    {
        //팝업 창을 띄우고, 사용자의 입력 -> 확인 버튼 눌렀을 시에 -> 사용자 입력값 기반으로 방 생성
        //방 제목, 1:1 or 2:2
        Debug.Log("fefe");
        Main.UIManager.OpenPopup(_setRoomBlock);
    }

    //방 리스트 패널에서 특정 방을 클릭 했을때 호출 되는 함수
    private void OnEnterRoomClicked(PointerEventData pointerEventData)
    {
        //해당 방의 제목으로 방을 찾아서 join
        if (PhotonNetwork.JoinRoom(pointerEventData.pointerClick.transform.Find("RoomName").GetComponent<TMP_Text>().text))
        {
            Main.NetworkManager.photonRoomDict.Clear();
        }
        else
        {
            string alertMessage = "현재 방이 가득 차 있습니다";
            Util.ShowAlert(alertMessage, transform);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Util.ShowAlert(message, transform);
    }

    //로비에 있을 때 제3자가 방을 생성하면 로비에 해당 방이 반영이 되도록 해주는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        

        foreach (RoomInfo room in roomList)
        {
            //방폭된 방 생성 방지
            if (room.RemovedFromList)
            {
                Main.NetworkManager.photonRoomDict[room.Name] = false;
                Main.ResourceManager.Destroy(_roomListContent.transform.Find($"{room.Name}Room")?.gameObject);
                continue;
            }
            //내 로컬상에 이미 해당 방이 존재한다면 생성 금지
            if (Main.NetworkManager.photonRoomDict.TryGetValue(room.Name, out bool isContain))
            {
                if (_lobbyInfoTxt != null) _lobbyInfoTxt.text = $"접속인원: {PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}명";
                _roomListContent.transform.Find($"{room.Name}Room").Find("RoomInfo").GetComponent<TMP_Text>().text = $"{room.PlayerCount} / {room.MaxPlayers}";
                if (isContain) continue;
            }
            if (_lobbyInfoTxt != null) _lobbyInfoTxt.text = $"접속인원: {PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}명";

            GameObject newRoom = Main.ResourceManager.Instantiate("UI_Prefabs/Rooms", _roomListContent.transform, $"{room.Name}Room");
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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        
        Util.ShowAlert(message, transform);
    }
}
