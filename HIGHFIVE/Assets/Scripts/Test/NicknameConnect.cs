using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NicknameConnect : MonoBehaviour
{
    private GoogleSheetManager _googleSheetManager;
    private TMP_InputField _nicknameField;
    private string _nickname;

    public GameObject ConnectingPanel; //로딩패널

    private void Awake()
    {
        _googleSheetManager = FindObjectOfType<GoogleSheetManager>();
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
    }

    public void Connect()
    {
        ConnectingPanel.SetActive(true);
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
