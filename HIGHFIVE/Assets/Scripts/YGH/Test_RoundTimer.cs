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

    TMP_Text TeamAScore;
    TMP_Text TeamBScore;

    public GameObject teamA;
    public GameObject teamB;

    //int TeamAscore = Test_RoundLogic.

    // int minute;
    int second;

    private void Awake()
    {
        TeamAScore = teamA.GetComponent<TMP_Text>();
        TeamBScore = teamB.GetComponent<TMP_Text>();
        curTime = farmingTime;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
        yield return StartCoroutine(FarmingTimer());

        if (curTime == battleTime)
        {
            yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            yield return StartCoroutine(BattleTimer());
        }

        int resultA;
        int resultB;
        int.TryParse(TeamAScore.text, out resultA);
        int.TryParse(TeamBScore.text, out resultB);


        if (resultA == 1 || resultB == 1)
        {
            yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            yield return StartCoroutine(FarmingTimer());
        }

        if ((resultA == 2) || (resultB == 2))
        {
            StopAllCoroutines();
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
