using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Test_RoundTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float farmingTime;
    [SerializeField] private float battleTime;
    [SerializeField] private float curTime;

    // int minute;
    int second;

    private void Awake()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        farmingTime = 10;
        battleTime = 5;
        curTime = farmingTime;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            // minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = second.ToString("0");
            yield return null;

            if (curTime <= 0)
            {
                curTime = 0;
                curTime = battleTime;

                //StartCoroutine(BattleTimer());
                yield break;
            }
        }
    }

    //IEnumerator BattleTimer()
    //{

    //}
}
