using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System;
using UnityEngine.UI;


public class NicknameConnect : MonoBehaviour
{
    private GoogleSheetManager _googleSheetManager;
    private TMP_InputField _nicknameField;
    private string _nickname;
    private Transform _canvasUI;

    public GameObject ConnectingPanel; //로딩패널

    private void Awake()
    {
        _googleSheetManager = FindObjectOfType<GoogleSheetManager>();
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        _canvasUI = GameObject.Find("StartSceneUI").GetComponent<Transform>();
    }

    private void Start()
    {
        _nicknameField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string value)
    {
        //Debug.Log("변경감지! 현재 입력된 텍스트: " + value);
    }


    // 닉네임형식검사
    bool SetNicknamePass()
    {
        // 앞뒤 공백 제거
        _nickname = _nicknameField.text.Trim();

        // 2~5의 영어숫자가 아니라면 false;
        string pattern = @"^[a-zA-Z가-힣]{2,5}$";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(_nickname))
        {
            string alertMessage = "닉네임 형식을 맟춰주세요.\n(공백x, 자음만x, 2 ~ 5글자)";
            Util.ShowAlert(alertMessage, _canvasUI);
            //Debug.Log("한글,영어 2~5글자를 넣어주세요.");
            return false;
        }

        else return true;
    }

    // 닉네임 확인 버튼에 할당해줘야함
    public void Connect()
    {
        _nickname = _nicknameField.text;
        //Debug.Log("닉네임 : " + _nickname + "\n접속시도");

        // 형식검사 통과하면 코루틴 실행
        if (SetNicknamePass()) StartCoroutine(NicknameCheckAndPhotonConnect()); 
    }

    // 닉네임중복검사 및 포톤연결
    IEnumerator NicknameCheckAndPhotonConnect()
    {
        ConnectingPanel.SetActive(true); // 로딩씬 띄워주기
        yield return StartCoroutine(_googleSheetManager.NicknameLogin()); // 중복검사 및 구글연결

        // 구글시트에서 확인해봤더니 중복인 닉네임일때
        if (_googleSheetManager.GD.result == "AlreadyUsed")
        {
            ConnectingPanel.SetActive(false);
            string alertMessage = "해당 닉네임이 이미 존재합니다. 다른 닉네임으로 설정해주세요.";
            Util.ShowAlert(alertMessage, _canvasUI);
            yield break;
        }

        // 중복이 아니라면 포톤연결
        Main.NetworkManager.Connect(_nickname);
    }
}
