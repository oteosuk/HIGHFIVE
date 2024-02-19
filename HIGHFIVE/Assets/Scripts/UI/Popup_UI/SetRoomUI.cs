using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetRoomUI : UIBase
{
    private enum Buttons
    {
        RecognizeBtn,
        ExitBtn
    }

    private enum GameObjects
    {
        SetTitleField,
        SetNumberDropdown
    }

    private Button _exitButton;
    private Button _recongnizeButton;
    private GameObject _roomtitleField;
    private GameObject _roomNumberDropdown;
    private int _roomNumber = 2;
    private bool isClicked;

    private void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        Bind<GameObject>(typeof(GameObjects),true);

        _exitButton = Get<Button>((int)Buttons.ExitBtn);
        _recongnizeButton = Get<Button>((int)Buttons.RecognizeBtn);
        _roomtitleField = Get<GameObject>((int)GameObjects.SetTitleField);
        _roomNumberDropdown = Get<GameObject>((int)GameObjects.SetNumberDropdown);
        isClicked = false;

        AddUIEvent(_recongnizeButton.gameObject, Define.UIEvent.Click, OnRecognizeButtonClicked);
        AddUIEvent(_exitButton.gameObject, Define.UIEvent.Click, OnExitButtonClicked);
        _roomNumberDropdown.GetComponent<TMP_Dropdown>().onValueChanged.AddListener((int index) => OnDropdownValueChanged(index));
    }


    private void OnRecognizeButtonClicked(PointerEventData pointerEventData)
    {
        string roomTitle = _roomtitleField.GetComponent<TMP_InputField>().text;
        string trimmedString = roomTitle.Trim();
        if (isClicked) return;
        isClicked = true;

        if (trimmedString == "")
        {
            string alertMessage = "방 제목을 입력해주세요";
            Util.ShowAlert(alertMessage, transform);
            isClicked = false;
            return;
        }

        if (trimmedString.Length > 10)
        {
            string alertMessage = "방 제목은 최대 10자까지 가능합니다";
            Util.ShowAlert(alertMessage, transform);
            isClicked = false;
            return;
        }

        if (!Main.NetworkManager.MakeRoom(trimmedString, _roomNumber))
        {
            string alertMessage = "해당 방이 현재 존재합니다. 다른 이름으로 방 제목을 설정해주세요";
            Util.ShowAlert(alertMessage, transform);
            isClicked = false;
            return;
        }
        else
        {
            Main.UIManager.CloseCurrentPopup(gameObject);
        }
        
        Main.NetworkManager.photonRoomDict.Clear();
    }

    private void OnExitButtonClicked(PointerEventData pointerEventData)
    {
        Main.UIManager.CloseCurrentPopup(gameObject);
    }

    //하드코딩
    private void OnDropdownValueChanged(int index)
    {
        switch(index)
        {
            case 0:
                _roomNumber = 2;
                break;
            case 1:
                _roomNumber = 4;
                break;
        }
    }
}
