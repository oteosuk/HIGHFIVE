using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class RoundLogic : MonoBehaviour
{
    public int currentRound = 1;

    [SerializeField] TMP_Text roundTxt;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;

    private GameSceneController gameSceneController;

    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    void Start()
    {
        gameSceneController = gameObject.GetComponent<GameSceneController>();
        gameSceneController.winEvent += PlayerWin;
    }

    public void RoundIndex()
    {
        int scoreRed;
        int scoreBlue;
        int.TryParse(TeamRedScore.text, out scoreRed);
        int.TryParse(TeamBlueScore.text, out scoreBlue);

        roundTxt.text = currentRound.ToString();

        if (scoreRed == 1 || scoreBlue == 1)
        {
            currentRound = 2;
            roundTxt.text = currentRound.ToString();
        }
        if (scoreRed == 1 && scoreBlue == 1)
        {
            currentRound = 3;
            roundTxt.text = currentRound.ToString();
        }
        if (scoreRed == 2 || scoreBlue == 2)
        {
            currentRound = 3;
            roundTxt.text = currentRound.ToString();
        }
    }

    public void GameOver()
    {
        VictoryPanel.SetActive(true);
    }

    // 테스트용 버튼
    public void TeamRedWinBtn()
    {
        gameSceneController.FinishRound(Define.Camp.Red);
    }

    public void TeamBlueWinBtn()
    {
        gameSceneController.FinishRound(Define.Camp.Blue);
    }

    // Win 이벤트 호출
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
