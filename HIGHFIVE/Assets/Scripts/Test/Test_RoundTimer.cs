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
        curTime = farmingTime;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        if (curTime == farmingTime)
        {
            yield return new WaitForSeconds(3.0f); // 화면 전환을 위해 잠깐 기다림
            yield return StartCoroutine(FarmingTimer());
        }
        if (curTime == battleTime) 
        {
            yield return new WaitForSeconds(3.0f); // 화면 전환을 위해 잠깐 기다림
            yield return StartCoroutine(BattleTimer());
        }
    }

    IEnumerator FarmingTimer()
    {
        farmingTime = 10;
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
                yield break;
            }
        }
    }

    IEnumerator BattleTimer()
    {
        battleTime = 5;
        curTime = battleTime;
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
                curTime = farmingTime;
                yield break;
            }
        }
    }
}
