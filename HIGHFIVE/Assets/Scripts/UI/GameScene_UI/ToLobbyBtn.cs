using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLobbyBtn : MonoBehaviour
{
    public void OnClickToLobby()
    {
        Main.NetworkManager.photonPlayerDict.Clear();
        Main.NetworkManager.photonRoomDict.Clear();
        Main.NetworkManager.photonReadyImageDict.Clear();
        Main.NetworkManager.photonPlayerObject.Clear();
        Main.NetworkManager.photonPlayer.Clear();

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", false }
        });
        PhotonNetwork.LeaveRoom();
    }
}
