using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartSceneLoginTest : MonoBehaviour
{
    private TMP_InputField _nicknameField;
    private string nickname;
    private GoogleSheetManager googleSheetManager; // GoogleSheetManager 스크립트의 인스턴스 변수
    public GameObject goBtn;
    public GameObject checkBtn;
    public GameObject nicknameInput;
    private bool isCheck;

    private void Awake()
    {
        isCheck = false;
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        googleSheetManager = FindObjectOfType<GoogleSheetManager>();
    }

    private void Update()
    {
        if(googleSheetManager.GD.result == "OK" && !isCheck)
        {
            isCheck = true;
            nicknameInput.SetActive(false);
            checkBtn.SetActive(false);
            goBtn.SetActive(true);
        }
    }

    public void NickNameCheckBtn()
    {
        googleSheetManager.NicknameLogin();
        nickname = _nicknameField.text;
        Debug.Log(nickname);
    }

    public void PhotonConnectBtn()
    {
        Main.NetworkManager.Connect(nickname);
    }
    // 닉네임 중복검사가 실시되고난후 다음씬 넘어가게
}
