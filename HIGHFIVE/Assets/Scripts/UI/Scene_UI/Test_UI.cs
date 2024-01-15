using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_UI : UIBase
{
    private enum Texts
    {
        WarriorName,
        WarriorInfo,
        WarriorAtk,
        WarriorDef,
        WarriorHp,
        WarriorAtkRange,
        WarriorAtkSpeed,
        WarriorMovSpeed,

        RogueName,
        RogueInfo,
        RogueAtk,
        RogueDef,
        RogueHp,
        RogueAtkRange,
        RogueAtkSpeed,
        RogueMovSpeed,

        MageName,
        MageInfo,
        MageAtk,
        MageDef,
        MageHp,
        MageAtkRange,
        MageAtkSpeed,
        MageMovSpeed,
    }

    private TMP_Text _warriornameTxt;
    private TMP_Text _warriorinfoTxt;
    private TMP_Text _warrioratkTxt;
    private TMP_Text _warriordefTxt;
    private TMP_Text _warriorhpTxt;
    private TMP_Text _warrioratkrangeTxt;
    private TMP_Text _warrioratkspeedTxt;
    private TMP_Text _warriormovspeedTxt;

    private TMP_Text _roguenameTxt;
    private TMP_Text _rogueinfoTxt;
    private TMP_Text _rogueatkTxt;
    private TMP_Text _roguedefTxt;
    private TMP_Text _roguehpTxt;
    private TMP_Text _rogueatkrangeTxt;
    private TMP_Text _rogueatkspeedTxt;
    private TMP_Text _roguemovspeedTxt;

    private TMP_Text _magenameTxt;
    private TMP_Text _mageinfoTxt;
    private TMP_Text _mageatkTxt;
    private TMP_Text _magedefTxt;
    private TMP_Text _magehpTxt;
    private TMP_Text _mageatkrangeTxt;
    private TMP_Text _mageatkspeedTxt;
    private TMP_Text _magemovspeedTxt;


    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts), true);

        // enum 과 변수 연결
        _warriornameTxt = Get<TMP_Text>((int)Texts.WarriorName).GetComponent<TMP_Text>();
        _warriorinfoTxt = Get<TMP_Text>((int)Texts.WarriorInfo).GetComponent<TMP_Text>();
        _warrioratkTxt = Get<TMP_Text>((int)Texts.WarriorAtk).GetComponent<TMP_Text>();
        _warriordefTxt = Get<TMP_Text>((int)Texts.WarriorDef).GetComponent<TMP_Text>();
        _warriorhpTxt = Get<TMP_Text>((int)Texts.WarriorHp).GetComponent<TMP_Text>();
        _warrioratkrangeTxt = Get<TMP_Text>((int)Texts.WarriorAtkRange).GetComponent<TMP_Text>();
        _warrioratkspeedTxt = Get<TMP_Text>((int)Texts.WarriorAtkSpeed).GetComponent<TMP_Text>();
        _warriormovspeedTxt = Get<TMP_Text>((int)Texts.WarriorMovSpeed).GetComponent<TMP_Text>();

        _roguenameTxt = Get<TMP_Text>((int)Texts.RogueName).GetComponent<TMP_Text>();
        _rogueinfoTxt = Get<TMP_Text>((int)Texts.RogueInfo).GetComponent<TMP_Text>();
        _rogueatkTxt = Get<TMP_Text>((int)Texts.RogueAtk).GetComponent<TMP_Text>();
        _roguedefTxt = Get<TMP_Text>((int)Texts.RogueDef).GetComponent<TMP_Text>();
        _roguehpTxt = Get<TMP_Text>((int)Texts.RogueHp).GetComponent<TMP_Text>();
        _rogueatkrangeTxt = Get<TMP_Text>((int)Texts.RogueAtkRange).GetComponent<TMP_Text>();
        _rogueatkspeedTxt = Get<TMP_Text>((int)Texts.RogueAtkSpeed).GetComponent<TMP_Text>();
        _roguemovspeedTxt = Get<TMP_Text>((int)Texts.RogueMovSpeed).GetComponent<TMP_Text>();

        _magenameTxt = Get<TMP_Text>((int)Texts.MageName).GetComponent<TMP_Text>();
        _mageinfoTxt = Get<TMP_Text>((int)Texts.MageInfo).GetComponent<TMP_Text>();
        _mageatkTxt = Get<TMP_Text>((int)Texts.MageAtk).GetComponent<TMP_Text>();
        _magedefTxt = Get<TMP_Text>((int)Texts.MageDef).GetComponent<TMP_Text>();
        _magehpTxt = Get<TMP_Text>((int)Texts.MageHp).GetComponent<TMP_Text>();
        _mageatkrangeTxt = Get<TMP_Text>((int)Texts.MageAtkRange).GetComponent<TMP_Text>();
        _mageatkspeedTxt = Get<TMP_Text>((int)Texts.MageAtkSpeed).GetComponent<TMP_Text>();
        _magemovspeedTxt = Get<TMP_Text>((int)Texts.MageMovSpeed).GetComponent<TMP_Text>();


        // DataManager에서 직업별 정보 불러오기
        if (!Main.DataManager.CharacterDict.TryGetValue("전사", out CharacterDBEntity warrior)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("마법사", out CharacterDBEntity mage)) return;


        // 엔진 상의 오브젝트와 데이터 연결하기
        _warriornameTxt.text = warrior.job;
        _warriorinfoTxt.text = warrior.info;
        _warrioratkTxt.text = warrior.atk.ToString();
        _warriordefTxt.text = warrior.def.ToString();
        _warriorhpTxt.text = warrior.maxHp.ToString();
        _warrioratkrangeTxt.text = warrior.atkRange.ToString();
        _warrioratkspeedTxt.text = warrior.atkSpeed.ToString();
        _warriormovspeedTxt.text = warrior.movSpeed.ToString();

        _roguenameTxt.text = rogue.job;
        _rogueinfoTxt.text = rogue.info;
        _rogueatkTxt.text = rogue.atk.ToString();
        _roguedefTxt.text = rogue.def.ToString();
        _roguehpTxt.text = rogue.maxHp.ToString();
        _rogueatkrangeTxt.text = rogue.atkRange.ToString();
        _rogueatkspeedTxt.text = rogue.atkSpeed.ToString();
        _roguemovspeedTxt.text = rogue.movSpeed.ToString();

        _magenameTxt.text = mage.job;
        _mageinfoTxt.text = mage.info;
        _mageatkTxt.text = mage.atk.ToString();
        _magedefTxt.text = mage.def.ToString();
        _magehpTxt.text = mage.maxHp.ToString();
        _mageatkrangeTxt.text = mage.atkRange.ToString();
        _mageatkspeedTxt.text = mage.atkSpeed.ToString();
        _magemovspeedTxt.text = mage.movSpeed.ToString();

    }
}
