using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.UI;

public class Test_RoundLogic : MonoBehaviour
{
    public int currentRound = 1;
    public int totalRounds = 3;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;


    private GameSceneController gameSceneController;

    void Start()
    {
        gameSceneController = gameObject.GetComponent<GameSceneController>();
        gameSceneController.winEvent += PlayerWin;
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

    // 종료 메서드
    // if()
    // {
    //    TeamRedWinBtn();
    // }    

    // 테스트용 버튼
    public void TeamRedWinBtn()
    {
        gameSceneController.FinishRound(Define.Camp.Red);
    }

    public void TeamBlueWinBtn()
    {
        gameSceneController.FinishRound(Define.Camp.Blue);
    }

    public void PlayerWin(Define.Camp winner)
    {
        if(winner == Define.Camp.Red)
        {
            int teamRedscore;
            int.TryParse(TeamRedScore.text, out teamRedscore);
            TeamRedScore.text = $"{++teamRedscore}";
        }
        else if (winner == Define.Camp.Blue)
        {
            int teamBluescore;
            int.TryParse(TeamBlueScore.text, out teamBluescore);
            TeamBlueScore.text = $"{++teamBluescore}";
        }
    }
}
