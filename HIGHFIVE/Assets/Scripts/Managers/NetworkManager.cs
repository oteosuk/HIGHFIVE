using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkManager
{
    public Dictionary<string, Player> photonPlayerDict = new Dictionary<string, Player>();
    public Dictionary<string, bool> photonRoomDict = new Dictionary<string, bool>();
    public Dictionary<string, Image> photonReadyImageDict = new Dictionary<string, Image>();

    private string _gameVersion = "1";
        

    //닉네임: 스타트씬에서 사용자에게 받은 닉네임 정보
    public void Connect(string nickname)
    {
        //방장이 씬을 옮기면 게스트도 같이 씬을 옮길 수 있게
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;

        //실제 포톤에 들어온 사용자의 이름
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }


    public bool MakeRoom(string name, int roomNumber)
    {
        return PhotonNetwork.CreateRoom(name, new RoomOptions
        { 
            MaxPlayers = roomNumber, 
            IsOpen = true, IsVisible = true, 
            CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "IsGameStarted", false } } 
        });
    }
}
