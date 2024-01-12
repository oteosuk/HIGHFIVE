using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectScene : BaseScene
{
    [SerializeField]
    private TMP_Text _rogueNameTxt;
    [SerializeField]
    private TMP_Text _warriorNameTxt;
    [SerializeField]
    private TMP_Text _mageNameTxt;
    protected override void Init()
    {
        base.Init();

        if (!Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("전사", out CharacterDBEntity warrior)) return;
        if (!Main.DataManager.CharacterDict.TryGetValue("마법사", out CharacterDBEntity mage)) return;

        _rogueNameTxt.text = rogue.job;
        _warriorNameTxt.text = warrior.job;
        _mageNameTxt.text = mage.job;
    }
}
