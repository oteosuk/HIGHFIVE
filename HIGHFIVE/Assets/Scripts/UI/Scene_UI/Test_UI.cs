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
        RogueName,
        MageName
    }

    private TMP_Text _warriorTxt;
    private TMP_Text _rogueTxt;
    private TMP_Text _mageTxt;

    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts), true);

        _warriorTxt = Get<TMP_Text>((int)Texts.WarriorName).GetComponent<TMP_Text>();
        _rogueTxt = Get<TMP_Text>((int)Texts.RogueName).GetComponent<TMP_Text>();
        _mageTxt = Get<TMP_Text>((int)Texts.MageName).GetComponent<TMP_Text>();


        if (!Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("전사", out CharacterDBEntity warrior)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("마법사", out CharacterDBEntity mage)) return;

        _warriorTxt.text = warrior.job;
        _rogueTxt.text = rogue.job;
        _mageTxt.text = mage.job;
    }
}
