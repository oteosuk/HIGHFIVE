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

        //Player[] currentRoomPlayer = PhotonNetwork.PlayerList;
        //Debug.Log(currentRoomPlayer.Length);
        //foreach (Player player in currentRoomPlayer)
        //{
        //    Main.NetworkManager.photonPlayerDict[player.NickName] = null;
        //}

        //Main.NetworkManager.photonRoomDict[PhotonNetwork.CurrentRoom.Name] = false;
        //Main.NetworkManager.photonReadyImageDict.Remove(PhotonNetwork.NickName);

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsReady", false }
        });
        PhotonNetwork.LeaveRoom();
    }
}
