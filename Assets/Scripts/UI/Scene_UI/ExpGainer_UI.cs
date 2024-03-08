using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpGainer_UI : UIBase
{
    private enum GameObjects
    {
        ExpBar
    }
    private StatController _statController;
    private void Start()
    {
        Bind<GameObject>(typeof(GameObjects), true);
        _statController = Main.GameManager.SpawnedCharacter.GetComponent<StatController>();
        _statController.expChangeEvent += SetExpRatio;
    }

    private void SetExpRatio(int curExp, int maxExp)
    {
        float ratio = curExp / (float)maxExp;
        Get<GameObject>((int)GameObjects.ExpBar).GetComponent<Slider>().value = ratio;
    }
}
