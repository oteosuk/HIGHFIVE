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
    //public GameObject goBtn; // 닉네임 연결되면 나타날 GO버튼
    public GameObject checkBtn; // 닉네임 연결되면 사라질 확인버튼
    public GameObject nicknameInput; // 닉네임 연결되면 사라질 인풋필드
    public GameObject connectingPanel; // 연결 중일 때 보일 패널
    //private bool isCheck; // 닉네임이 중복체킹됐는지 확인하는 bool변수

    private void Awake()
    {
        //isCheck = false;
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        googleSheetManager = FindObjectOfType<GoogleSheetManager>();
    }

    /*private void Update()
    {
        if(googleSheetManager.GD.result == "OK" && !isCheck)
        {
            isCheck = true;
            nicknameInput.SetActive(false);
            checkBtn.SetActive(false);
            //goBtn.SetActive(true);
        }
    }*/
    
    /*public void NickNameCheckBtn()
    {
        googleSheetManager.NicknameLogin();
        nickname = _nicknameField.text;
        Debug.Log(nickname);
    }

    public void PhotonConnectBtn()
    {
        connectingPanel.SetActive(true); // 연결 중 패널 활성화
        Main.NetworkManager.Connect(nickname);
    }*/
    public void Connect()
    {
        nickname = _nicknameField.text;
        Debug.Log(nickname);
        connectingPanel.SetActive(true); // 연결 중 패널 활성화
        StartCoroutine(NicknameCheckAndPhotonConnect());
    }

    IEnumerator NicknameCheckAndPhotonConnect()
    {
        yield return StartCoroutine(googleSheetManager.NicknameLogin());
        //만약 닉네임이 중복이라면 중복판넬을 띄워주고 yield break;
        Main.NetworkManager.Connect(nickname);
    }
}
