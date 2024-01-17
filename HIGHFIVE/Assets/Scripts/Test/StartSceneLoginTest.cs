using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class StartSceneLoginTest : MonoBehaviour
{
    private TMP_InputField _nicknameField;
    private string nickname;

    private void Awake()
    {
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        nickname = _nicknameField.text;
    }

    public void OnButtonClick()
    {
        Debug.Log(Main.DataManager.CharacterDict.Count);
        Main.NetworkManager.Connect(nickname);
    }
}
