using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    private Button _infoBtn;
    private Button _settingBtn;

    private Button _infoExitBtn;
    private Button _settingExitBtn;

    private GameObject _infoPanel;
    private GameObject _settingPanel;

    private enum Buttons
    {
        InfoBtn,
        InfoExitBtn,
        SettingBtn,
        SettingExitBtn,
    }

    private enum GameObjects
    {
        InfoPanel,
        SettingPanel
    }
    void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        _infoBtn = Get<Button>((int)Buttons.InfoBtn);
        _settingBtn = Get<Button>((int)Buttons.SettingBtn);

        _infoExitBtn = Get<Button>((int)Buttons.InfoExitBtn);
        _settingExitBtn = Get<Button>((int)Buttons.SettingExitBtn);

        Bind<GameObject>(typeof(GameObjects), true);
        _infoPanel = Get<GameObject>((int)GameObjects.InfoPanel);
        _settingPanel = Get<GameObject>((int)GameObjects.SettingPanel);

        AddUIEvent(_infoBtn.gameObject, Define.UIEvent.Click, OninfoBtnClicked);
        AddUIEvent(_settingBtn.gameObject, Define.UIEvent.Click, OnsettingBtnClicked);
        AddUIEvent(_infoExitBtn.gameObject, Define.UIEvent.Click, OninfoExitBtnClicked);
        AddUIEvent(_settingExitBtn.gameObject, Define.UIEvent.Click, OnsettingExitBtnClicked);
    }

    private void OnsettingExitBtnClicked(PointerEventData data)
    {
        Main.UIManager.CloseCurrentPopup(_settingPanel);
    }

    private void OninfoExitBtnClicked(PointerEventData data)
    {
        Main.UIManager.CloseCurrentPopup(_infoPanel);
    }

    private void OnsettingBtnClicked(PointerEventData data)
    {
        Main.UIManager.CloseCurrentPopup(_infoPanel);
        Main.UIManager.OpenPopup(_settingPanel);
    }

    private void OninfoBtnClicked(PointerEventData pointerEventData)
    {
        Main.UIManager.CloseCurrentPopup(_settingPanel);
        Main.UIManager.OpenPopup(_infoPanel);
    }
}
