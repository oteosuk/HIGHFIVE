using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionUI : UIBase
{
    private enum Buttons
    {
        RuleInfoBtn,
        RuleInfoExitBtn,
        KeyInfoBtn,
        KeyInfoExitBtn,
        SettingBtn,
        SettingExitBtn
    }

    private enum GameObjects
    {
        RuleInfoBackPanel,
        KeyInfoBackPanel,
        SettingBackPanel
    }
    private Button _ruleInfoBtn;
    private Button _keyInfoBtn;
    private Button _settingInfoBtn;

    private Button _ruleInfoExitBtn;
    private Button _keyInfoExitBtn;
    private Button _settingInfoExitBtn;

    private GameObject _keyInfoPanel;
    private GameObject _rullInfoPanel;
    private GameObject _settingPanel;
    void Start()
    {
        Bind<Button>(typeof(Buttons), true);
        _ruleInfoBtn = Get<Button>((int)Buttons.RuleInfoBtn);
        _keyInfoBtn = Get<Button>((int)Buttons.KeyInfoBtn);
        _settingInfoBtn = Get<Button>((int)Buttons.SettingBtn);

        _ruleInfoExitBtn = Get<Button>((int)Buttons.RuleInfoExitBtn);
        _keyInfoExitBtn = Get<Button>((int)Buttons.KeyInfoExitBtn);
        _settingInfoExitBtn = Get<Button>((int)Buttons.SettingExitBtn);

        Bind<GameObject>(typeof(GameObjects), true);
        _keyInfoPanel = Get<GameObject>((int)GameObjects.KeyInfoBackPanel);
        _rullInfoPanel = Get<GameObject>((int)GameObjects.RuleInfoBackPanel);
        _settingPanel = Get<GameObject>((int)GameObjects.SettingBackPanel);
        if (Main.SceneManagerEx.CurrentScene == Define.Scene.GameScene)
        {
            Main.UIManager.OpenPopup(_keyInfoPanel);
        }

        AddUIEvent(_ruleInfoBtn.gameObject, Define.UIEvent.Click, OninfoBtnClicked);
        AddUIEvent(_keyInfoBtn.gameObject, Define.UIEvent.Click, OninfoBtnClicked);
        AddUIEvent(_settingInfoBtn.gameObject, Define.UIEvent.Click, OninfoBtnClicked);

        AddUIEvent(_ruleInfoExitBtn.gameObject, Define.UIEvent.Click, OninfoExitBtnClicked);
        AddUIEvent(_keyInfoExitBtn.gameObject, Define.UIEvent.Click, OninfoExitBtnClicked);
        AddUIEvent(_settingInfoExitBtn.gameObject, Define.UIEvent.Click, OninfoExitBtnClicked);
    }

    private void OninfoExitBtnClicked(PointerEventData pointerEventData)
    {
        Main.UIManager.CloseCurrentPopup();
    }

    private void OninfoBtnClicked(PointerEventData pointerEventData)
    {
        switch(pointerEventData.selectedObject.name)
        {
            case "RuleInfoBtn":
                Main.UIManager.OpenPopup(_rullInfoPanel);
                break;
            case "KeyInfoBtn":
                Main.UIManager.OpenPopup(_keyInfoPanel);
                break;
            case "SettingBtn":
                Main.UIManager.OpenPopup(_settingPanel);
                break;
        }
    }
}
