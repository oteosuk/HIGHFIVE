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
    private GameFieldController _gameFieldController;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;

    // int minute;
    int second;

    private RoundLogic roundLogic;


    private void Start()
    {
        roundLogic = GetComponent<RoundLogic>();
        _gameFieldController = GetComponent<GameFieldController>();
        StartFarmingTimer();
    }

    private void Update()
    {
        if ((roundLogic._teamBlueScore == 2) || (roundLogic._teamRedScore == 2))
        {
            if (roundLogic._teamBlueScore == 2) { roundLogic.GameOver(Define.Camp.Blue); }
            if (roundLogic._teamRedScore == 2) { roundLogic.GameOver(Define.Camp.Red); }
            StopAllCoroutines();
        }
    }

    void StartFarmingTimer()
    {
        roundLogic.RoundIndex();
        StopCoroutine(BattleTimer());
        StartCoroutine(FarmingTimer());
    }

    void StartBattleTimer()
    {
        StopCoroutine(FarmingTimer());
        StartCoroutine(BattleTimer());
    }

    IEnumerator FarmingTimer()
    {
        yield return new WaitForSeconds(1.0f);
        Main.GameManager.page = Define.Page.Farming;
        farmingTime = 10; // 이부분 시간조절
        curTime = farmingTime;

        
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            // minute = (int)curTime / 60;
            second = (int)curTime % 60; // 근데 여기에서 분이 안나와버림
            text.text = second.ToString("0");
            yield return null;

            if (curTime <= 0)
            {
                _gameFieldController.CallBattleEvent();
                curTime = battleTime;
                StartBattleTimer();
                yield break;
            }
        }
    }

    IEnumerator BattleTimer()
    {
        yield return new WaitForSeconds(1.0f);
        Main.GameManager.page = Define.Page.Battle;
        battleTime = 20; // 이부분 시간조절
        curTime = battleTime;

        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            // minute = (int)curTime / 60;
            second = (int)curTime % 60; // 근데 여기에서 분이 안나와버림
            text.text = second.ToString("0");
            yield return null;


            if (curTime <= 0)
            {
                _gameFieldController.CallFarmingEvent();
                StartFarmingTimer();
                yield break;
            }
        }
    }
}