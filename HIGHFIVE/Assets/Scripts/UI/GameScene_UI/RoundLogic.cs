using Photon.Pun;
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
    [SerializeField] Transform _redBattleSpawnZone;
    [SerializeField] Transform _blueBattleSpawnZone;
    [SerializeField] Transform _redFarmingSpawnZone;
    [SerializeField] Transform _blueFarmingSpawnZone;

    private GameFieldController _gameSceneController;

    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    void Start()
    {

        _gameSceneController = gameObject.GetComponent<GameFieldController>();
        _gameSceneController.winEvent += PlayerWin;
        _gameSceneController.battleEvent += ChangeToBattleField;
        _gameSceneController.farmingEvent += ChangeToFarmingField;

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

    public void GameOver(Define.Camp winCamp)
    {
        if (winCamp == Define.Camp.Red)
        {
            if (Main.GameManager.SelectedCamp == Define.Camp.Red) { VictoryPanel.SetActive(true); }
            else { DefeatPanel.SetActive(true); }
        }
        else
        {
            if (Main.GameManager.SelectedCamp == Define.Camp.Blue) { VictoryPanel.SetActive(true); }
            else { DefeatPanel.SetActive(true); }
        }
    }

    // 테스트용 버튼
    public void TeamRedWin()
    {
        _gameSceneController.FinishRound(Define.Camp.Red);
    }

    public void TeamBlueWin()
    {
        _gameSceneController.FinishRound(Define.Camp.Blue);
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

    public void ChangeToBattleField()
    {
        Main.GameManager.SpawnedCharacter.transform.position = Main.GameManager.SelectedCamp == Define.Camp.Red ? _redBattleSpawnZone.position : _blueBattleSpawnZone.position;
        ChangeToField();
    }
    public void ChangeToFarmingField()
    {
        if (CheckWinner() == Define.Camp.Red) { TeamRedWin(); }
        else { TeamBlueWin(); }
        Main.GameManager.SpawnedCharacter.transform.position = Main.GameManager.SelectedCamp == Define.Camp.Red ? _redFarmingSpawnZone.position : _blueFarmingSpawnZone.position;
        ChangeToField();
    }

    private void ChangeToField()
    {
        Character character = Main.GameManager.SpawnedCharacter;
        character._playerStateMachine.moveInput = character.transform.position;
        character.stat.CurHp = character.stat.MaxHp;
        character.GetComponent<PhotonView>().RPC("SetHpRPC", RpcTarget.All, character.stat.CurHp);
        Camera.main.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10);
    }


    private Define.Camp CheckWinner()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        List<Character> redPlayerList = new List<Character>();
        List<Character> bluePlayerList = new List<Character>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].layer == (int)Define.Camp.Red)
            {
                redPlayerList.Add(players[i].GetComponent<Character>());
            }
            if (players[i].layer == (int)Define.Camp.Blue)
            {
                bluePlayerList.Add(players[i].GetComponent<Character>());
            }
        }

        float redTeamHpRatio = 0;
        float blueTeamHpRatio = 0;
        foreach (Character redPlayer in redPlayerList)
        {
            redTeamHpRatio += redPlayer.stat.CurHp / (float)redPlayer.stat.MaxHp;
        }
        foreach (Character bluePlayer in bluePlayerList)
        {
            blueTeamHpRatio += bluePlayer.stat.CurHp / (float)bluePlayer.stat.MaxHp;
        }

        if (redTeamHpRatio > blueTeamHpRatio)
        {
            return Define.Camp.Red;
        }
        else if (redTeamHpRatio < blueTeamHpRatio)
        {
            return Define.Camp.Blue;
        }
        else
        {
            float randomValue = Random.value;
            if (randomValue < 0.5f)
            {
                return Define.Camp.Red;
            }
            else
            {
                return Define.Camp.Blue;
            }
        }
    }

    
}
