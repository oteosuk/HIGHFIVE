using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public List<Type> onBuffList = new List<Type>();
    public event Action<Type> buffEvent;

    public void AddBuffEvent(Type buffType)
    {
        if (buffEvent != null)
        {
            buffEvent.Invoke(buffType);
        }
    }
}
