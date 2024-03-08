using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PhotonHandler : MonoBehaviourPunCallbacks
{
    private GoogleSheetManager _google;
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
    }

    public override void OnCreatedRoom()
    {
        Main.SceneManagerEx.LoadScene(Define.Scene.RoomScene);
    }
}
