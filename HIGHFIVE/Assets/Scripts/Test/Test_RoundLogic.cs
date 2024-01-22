using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_RoundLogic : MonoBehaviour
{
    public int currentRound = 1;
    public int totalRounds = 3;

    public int TeamAScore = 0;
    public int TeamBScore = 0;

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        Debug.Log("Round" + currentRound);
    }

    void EndRound()
    {
        if (currentRound < totalRounds)
        {
            currentRound++;
            StartRound();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }


    // 테스트용 버튼
    public void TeamAWinBtn()
    {
        Debug.Log("TeamAWin");
    }

    public void TeamBWinBtn()
    {
        Debug.Log("TeamBWin");
    }

}
