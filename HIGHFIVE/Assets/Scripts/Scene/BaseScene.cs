using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Main.DataManager.Initialize();
    }
}
