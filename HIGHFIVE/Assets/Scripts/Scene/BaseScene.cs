using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Init();
    }

    protected virtual void Init() //씬 공통적으로 실행해야하는 부분
    {
        Main.DataManager.DataInit();
    }

    protected virtual void SetLayer(int layer)
    {
        Main.GameManager.SpawnedCharacter.gameObject.layer = layer;
    }
}
