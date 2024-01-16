using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    private bool _isInit = false;
    private void Start()
    {
        Init();
    }

    protected virtual void Init() //씬 공통적으로 실행해야하는 부분
    {
        if (_isInit) return;
        Main.DataManager.Initialize();
        _isInit = true;
        //DOTween.Init();
    }
}
