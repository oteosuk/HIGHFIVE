using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class photonLobbyTest : UIBase
{
    public TMP_Text lobbyInfoTxt;
    public override void OnJoinedLobby()
    {
        lobbyInfoTxt.text = $"접속인원: {PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms}명";
    }
}
