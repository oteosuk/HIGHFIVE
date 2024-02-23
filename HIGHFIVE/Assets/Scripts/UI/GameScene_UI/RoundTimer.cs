using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using Photon.Pun;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private int curTime;
    private int farmingTime;
    private int battleTime;
    private GameFieldController _gameFieldController;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;


    private RoundLogic roundLogic;
    private PhotonView _pv;


    private void Start()
    {
        battleTime = 20;
        farmingTime = 20;
        roundLogic = GetComponent<RoundLogic>();
        _gameFieldController = GetComponent<GameFieldController>();
        _pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            StartFarmingTimer();
        }
    }

    private void Update()
    {
        if ((roundLogic._teamBlueScore == 2) || (roundLogic._teamRedScore == 2))
        {
            if (roundLogic._teamBlueScore == 2) { roundLogic.GameOver(Define.Camp.Blue); }
            if (roundLogic._teamRedScore == 2) { roundLogic.GameOver(Define.Camp.Red); }
            if (PhotonNetwork.IsMasterClient)
            {
                StopAllCoroutines();
            }
        }
    }

    void StartFarmingTimer()
    {
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
        roundLogic.RoundIndex();
        _pv.RPC("SynPage", RpcTarget.All, (int)Define.Page.Farming);
        curTime = farmingTime;

        while (curTime > 0)
        {
            curTime -= 1;
            _pv.RPC("SyncTime", RpcTarget.All, curTime);
            yield return new WaitForSeconds(1);
        }
        StartBattleTimer();
    }

    IEnumerator BattleTimer()
    {
        _pv.RPC("SynPage", RpcTarget.All, (int)Define.Page.Battle);
        curTime = battleTime;

        while (curTime > 0)
        {
            curTime -= 1;
            if (curTime < battleTime - 5 && CheckPlayerObjDie())
            {
                yield return new WaitForSeconds(2);
                break;
            }
            _pv.RPC("SyncTime", RpcTarget.All, curTime);
            yield return new WaitForSeconds(1);
        }
        StartFarmingTimer();
    }

    private bool CheckPlayerObjDie()
    {
        int restRedTeamNum = 0;
        int restBlueTeamNum = 0;
        foreach (KeyValuePair<int, GameObject> playerObj in Main.NetworkManager.photonPlayerObject)
        {
            if (playerObj.Value?.layer == (int)Define.Layer.Red) { restRedTeamNum += 1; }
            if (playerObj.Value?.layer == (int)Define.Layer.Blue) { restBlueTeamNum += 1; }
        }
        if (restRedTeamNum == 0 || restBlueTeamNum == 0) { return true; }

        return false;
    }

    [PunRPC]
    private void SynPage(int pageNum)
    {
        if (pageNum == (int)Define.Page.Farming)
        {
            Main.GameManager.page = Define.Page.Farming;
            if (roundLogic.currentRound  > 1)
            {
                _gameFieldController.CallFarmingEvent();
            }
        }
        else 
        {
            Main.GameManager.page = Define.Page.Battle;
            _gameFieldController.CallBattleEvent();
        }
    }
    [PunRPC]
    private void SyncTime(int curTime)
    {
        text.text = curTime.ToString();
    }
}