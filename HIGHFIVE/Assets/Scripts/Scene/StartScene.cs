using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Debug.Log(Main.DataManager.CharacterDict.TryGetValue("전사", out CharacterDBEntity warrior));

        Debug.Log(warrior.def);
        Debug.Log(warrior.info);
    }
}
