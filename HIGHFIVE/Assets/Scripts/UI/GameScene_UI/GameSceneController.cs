using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public event Action<Define.Camp> winEvent;

    public void FinishRound(Define.Camp camp)
    {
        winEvent?.Invoke(camp);
    }
}
