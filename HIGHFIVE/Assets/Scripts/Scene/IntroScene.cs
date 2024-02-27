using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Main.SceneManagerEx.CurrentScene = Define.Scene.IntroScene;
        Main.SoundManager.PlayBGM("Battle_Normal_EW01_B");
    }
}