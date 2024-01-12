using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Main.DataManager.Initialize();
    }
}
