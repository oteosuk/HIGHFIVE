using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager
{
    public Dictionary<string, bool> photonPlayerDict = new Dictionary<string, bool>();
    public Dictionary<string, bool> photonRoomDict = new Dictionary<string, bool>();
    private string _gameVersion = "1";
    private byte _maxPlayersPerRoom;
        

    //닉네임: 스타트씬에서 사용자에게 받은 닉네임 정보
    public void Connect(string nickname)
    {
        //방장이 씬을 옮기면 게스트도 같이 씬을 옮길 수 있게
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;

        //실제 포톤에 들어온 사용자의 이름
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }



    //현재는 자기 닉네임을 가지고 방을 생성
    public void MakeRoom(string name)
    {
        PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true });
    }
}
