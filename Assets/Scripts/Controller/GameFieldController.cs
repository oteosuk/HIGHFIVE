using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldController : MonoBehaviour
{
    public event Action battleEvent;
    public event Action farmingEvent;
    public event Action<Define.Camp> winEvent;

    public void FinishRound(Define.Camp camp)
    {
        winEvent?.Invoke(camp);
    }

    public void CallBattleEvent()
    {
        if (battleEvent != null)
        {
            battleEvent.Invoke();
        }
    }
    public void CallFarmingEvent()
    {
        if (farmingEvent != null)
        {
            farmingEvent.Invoke();
        }
    }
}
