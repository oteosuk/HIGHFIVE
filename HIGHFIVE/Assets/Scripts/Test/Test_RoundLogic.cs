using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_RoundLogic : MonoBehaviour
{
    public int currentRound = 1;
    public int totalRounds = 3;

    public TextMeshProUGUI teamAScoreText;
    public TextMeshProUGUI teamBScoreText;

    public int teamAScore = 0;
    public int teamBScore = 0;

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
        teamAScore++;
        teamAScoreText.text = teamAScore.ToString();
    }

    public void TeamBWinBtn()
    {
        teamBScore++;
        teamBScoreText.text = teamBScore.ToString();
    }

}
