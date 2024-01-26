using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    public event Action winEvent;

    public int teamAScore;
    public int teamBScore;

    public void FinishRound()
    {
        // Evenet?.Invoke();
    }

}
