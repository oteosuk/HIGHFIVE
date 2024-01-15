using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonHandler : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Init();
    }
  
    private void Init()
    {
        GameObject gameObject = GameObject.Find("PhotonHandler");
        if (gameObject == null)
        {
            gameObject = new GameObject("PhotonHandler");
            gameObject.AddComponent<PhotonHandler>();
        }
        DontDestroyOnLoad(gameObject);
    }

    // 01.사용자가 포톤 서버에 커넥트 됐을때 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        TypedLobby typedLobby = new TypedLobby(null, LobbyType.Default);
        if (PhotonNetwork.JoinLobby(typedLobby))
        {
            Main.SceneManagerEx.LoadScene(Define.Scene.LobbyScene);
        }
        
        Debug.Log("01.포톤과 연결 되었습니다.");
    }

    //02.사용자가 로비에 들어왔을때 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        //ToDo: 스타트씬에서 로비씬으로 옮겨주는 작업을 해줘야겠죠

        Player[] players = PhotonNetwork.PlayerList;

        Debug.Log(players.Length); // -> 0
        // 각 플레이어의 닉네임 출력 또는 활용
        foreach (Player player in players)
        {
            string playerName = player.NickName;
            Debug.Log("플레이어 닉네임: " + playerName);
        }
        Debug.Log(PhotonNetwork.InLobby);
        Debug.Log(PhotonNetwork.CountOfPlayers);
        
        Debug.Log("02.로비에 들어오셨습니다.");
    }

    public override void OnCreatedRoom()
    {
        Main.SceneManagerEx.LoadScene(Define.Scene.RoomScene);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("hi");
    }
}
