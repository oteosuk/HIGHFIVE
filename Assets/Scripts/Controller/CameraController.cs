using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public event Action characterSpawnEvent;

    public void CallCharacterSpawnEvent()
    {
        if (characterSpawnEvent != null)
        {
            characterSpawnEvent.Invoke();
        }
    }
}