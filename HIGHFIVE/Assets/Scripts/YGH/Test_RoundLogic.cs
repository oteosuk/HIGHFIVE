using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test_RoundLogic : MonoBehaviour
{
    public int currentRound = 1;
    public int totalRounds = 3;

    //public TextMeshProUGUI teamAScoreText;
    //public TextMeshProUGUI teamBScoreText;

    //public int teamAScore = 0;
    //public int teamBScore = 0;

    TMP_Text TeamAScore;
    TMP_Text TeamBScore;

    public GameObject teamA;
    public GameObject teamB;


    void Start()
    {
        TeamAScore = teamA.GetComponent<TMP_Text>();
        TeamBScore = teamB.GetComponent<TMP_Text>();
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
        int resultA;
        int.TryParse(TeamAScore.text, out resultA);

        resultA++;

        TeamAScore.text = resultA.ToString();
    }

    public void TeamBWinBtn()
    {
        int resultB;
        int.TryParse(TeamBScore.text, out resultB);

        resultB++;

        TeamBScore.text = resultB.ToString();
    }

}
