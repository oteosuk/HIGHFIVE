using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        Main.SceneManagerEx.CurrentScene = Define.Scene.LobbyScene;
        Main.SoundManager.PlayBGM("Town_Castle_01");
    }
}
