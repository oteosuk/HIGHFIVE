using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private float curTime;
    private float farmingTime;
    private float battleTime;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;

    // int minute;
    int second;

    public RoundLogic roundLogic;

    private void Awake()
    {
        roundLogic.RoundIndex();
        curTime = farmingTime;
        StartCoroutine (FarmingTimer());
    }

    void StartFarmingTimer()
    {
        int scoreRed;
        int scoreBlue;
        int.TryParse(TeamRedScore.text, out scoreRed);
        int.TryParse(TeamBlueScore.text, out scoreBlue);

        if (scoreRed == 1 || scoreBlue == 1)
        {
            roundLogic.RoundIndex();

            StopCoroutine(BattleTimer());
            StartCoroutine(FarmingTimer());
        }

        else if (scoreRed == 1 && scoreBlue == 1)
        {
            roundLogic.RoundIndex();

            StopCoroutine(BattleTimer());
            StartCoroutine(FarmingTimer());
        }

        if ((scoreRed == 2) || (scoreBlue == 2))
        {
            roundLogic.RoundIndex();
            roundLogic.GameOver();
            StopAllCoroutines();
        }
    }

    void StartBattleTimer()
    {
        if (curTime == battleTime)
        {
            StopCoroutine(FarmingTimer());
            StartCoroutine(BattleTimer());
        } 
    }

    IEnumerator FarmingTimer()
    {
        //yield return new WaitForSeconds(1.0f); // 화면 전환을 위해 잠깐 기다림
        farmingTime = 6;
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
                curTime = battleTime;
                StartBattleTimer();
                yield break;
            }
        }
    }

    IEnumerator BattleTimer()
    {
        //yield return new WaitForSeconds(1.0f);
        battleTime = 4;
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
                farmingTime = 8;
                curTime = farmingTime;
                StartFarmingTimer();
                yield break;
            }
        }
    }
}