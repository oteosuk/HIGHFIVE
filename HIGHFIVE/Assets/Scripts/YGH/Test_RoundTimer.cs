using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Test_RoundTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private float farmingTime1;
    private float farmingTime2;
    private float farmingTime3;
    private float battleTime1;
    private float battleTime2;
    private float battleTime3;
    private float curTime;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;


    // int minute;
    int second;

    public Test_RoundLogic test_RoundLogic;

    private void Awake()
    {
        curTime = farmingTime1;
        StartCoroutine(StartTimer());
    }


    IEnumerator StartTimer()
        // if 조건문을 못 읽고 코루틴이 쭉 아래까지 내려가서 2라운드로 못넘어감
    {
        //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
        yield return StartCoroutine(FarmingTimer1());

        if (curTime == battleTime1)
        {
            //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            StopCoroutine(FarmingTimer1());
            StartCoroutine(BattleTimer1());
        }

        if (curTime == battleTime2)
        {
            //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            StopCoroutine(FarmingTimer2());
            StartCoroutine(BattleTimer2());
        }

        if (curTime == battleTime3)
        {
            //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            StopCoroutine(FarmingTimer3());
            StartCoroutine(BattleTimer3());
        }

        int scoreRed;
        int scoreBlue;
        int.TryParse(TeamRedScore.text, out scoreRed);
        int.TryParse(TeamBlueScore.text, out scoreBlue);

        if (scoreRed == 1 || scoreBlue == 1)
        {
            //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            StopCoroutine(BattleTimer1());

            Debug.Log("test2");

            test_RoundLogic.RoundIndex();


            yield return (FarmingTimer2());
        }

        if (scoreRed == 1 && scoreBlue == 1)
        {
            //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
            StopCoroutine(BattleTimer2());

            test_RoundLogic.RoundIndex();

            yield return StartCoroutine(FarmingTimer3());
        }

        if ((scoreRed == 2) || (scoreBlue == 2))
        {
            StopAllCoroutines();
            yield break;
        }
    }

    IEnumerator FarmingTimer1()
    {
        farmingTime1 = 8;
        curTime = farmingTime1;
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
                curTime = battleTime1;
            }
            // 점수 읽는 함수 호출 (점수 인식하는 메서드 필요)
        }
    }

    IEnumerator BattleTimer1()
    {
        battleTime1 = 5;
        curTime = battleTime1;
        Debug.Log(curTime);
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
                //curTime = farmingTime2;
            }
        }
    }

    IEnumerator FarmingTimer2()
    {
        farmingTime2 = 8;
        curTime = farmingTime2;
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
                curTime = battleTime2;
            }
        }
    }

    IEnumerator BattleTimer2()
    {
        battleTime2 = 5;
        curTime = battleTime2;
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
                //curTime = farmingTime3;
            }
        }
    }

    IEnumerator FarmingTimer3()
    {
        farmingTime3 = 8;
        curTime = farmingTime3;
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
                curTime = battleTime3;
            }
        }
    }

    IEnumerator BattleTimer3()
    {
        battleTime3 = 5;
        curTime = battleTime3;
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
            }
        }
    }
}