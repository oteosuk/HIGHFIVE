using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    // 01.사용자가 포톤 서버에 커넥트 됐을때 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("01.포톤과 연결 되었습니다.");
    }

    //02.사용자가 로비에 들어왔을때 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        //ToDo: 스타트씬에서 로비씬으로 옮겨주는 작업을 해줘야겠죠
        Main.SceneManagerEx.LoadScene(Define.Scene.LobbyScene);
        Debug.Log("02.로비에 들어오셨습니다.");
    }
    
}
