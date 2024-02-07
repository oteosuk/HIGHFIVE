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
        TypedLobby typedLobby = new TypedLobby(null, LobbyType.Default);
        if (PhotonNetwork.JoinLobby(typedLobby))
        {
            Main.SceneManagerEx.LoadScene(Define.Scene.LobbyScene);
        }
    }
}
