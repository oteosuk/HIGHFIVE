using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    //public event Action damageChangeEvent;
    //public event Action expChangeEvent;
    public event Action<int,int> hpChangeEvent;

    public void CallChangeHpEvent(int curHp, int maxHp)
    {
        if (hpChangeEvent != null)
        {
            hpChangeEvent.Invoke(curHp, maxHp);
        }
    }
}
