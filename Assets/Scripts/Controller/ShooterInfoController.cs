using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterInfoController : MonoBehaviour
{
    public event Action<GameObject> shooterInfoEvent;

    public void CallShooterInfoEvent(GameObject go)
    {
        if (shooterInfoEvent != null)
        {
            shooterInfoEvent.Invoke(go);
        }
    }
}
