using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLobbyBtn : MonoBehaviour
{
    public void OnClickToLobby()
    {
        SceneManager.LoadScene("02.LobbyScene");
    }
}
