using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartSceneLoginTest : MonoBehaviour
{
    private GoogleSheetManager _googleSheetManager;
    private TMP_InputField _nicknameField;
    private GameObject _connectingPanel; //로딩패널
    private string _nickname;

    private void Awake()
    {
        _googleSheetManager = FindObjectOfType<GoogleSheetManager>();
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        _connectingPanel = GameObject.Find("ConnectingPanel");
    }

    public void Connect()
    {
        _connectingPanel.SetActive(true);
        _nickname = _nicknameField.text;
        Debug.Log("닉네임 : " + _nickname);

        StartCoroutine(NicknameCheckAndPhotonConnect());
    }

    IEnumerator NicknameCheckAndPhotonConnect()
    {
        yield return StartCoroutine(_googleSheetManager.NicknameLogin());
        //만약 닉네임이 중복이라면 중복판넬을 띄워주고 yield break;@@@@@@@@@@@@@@@@@@
        Main.NetworkManager.Connect(_nickname);
    }
}
