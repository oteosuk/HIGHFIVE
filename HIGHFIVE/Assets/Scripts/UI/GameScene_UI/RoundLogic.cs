using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundLogic : MonoBehaviour
{
    public int currentRound = 0;

    [SerializeField] TMP_Text roundTxt;

    [SerializeField] TMP_Text TeamRedScore;
    [SerializeField] TMP_Text TeamBlueScore;
    [SerializeField] Transform _redBattleSpawnZone;
    [SerializeField] Transform _blueBattleSpawnZone;
    [SerializeField] Transform _redFarmingSpawnZone;
    [SerializeField] Transform _blueFarmingSpawnZone;

    private GameFieldController _gameSceneController;
    public int _teamRedScore;
    public int _teamBlueScore;

    public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    void Start()
    {
        _gameSceneController = gameObject.GetComponent<GameFieldController>();
        _gameSceneController.battleEvent += ChangeToBattleField;
        _gameSceneController.farmingEvent += ChangeToFarmingField;
        _gameSceneController.winEvent += PlayerWin;
    }

    public void RoundIndex()
    {
        currentRound++;
        GetComponent<PhotonView>().RPC("SyncRound", RpcTarget.All, currentRound);
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
        if (winner != Main.GameManager.SelectedCamp)
        {
            BaseBuff loserBuff = new LoserBuff();
            Main.GameManager.SpawnedCharacter.BuffController.AddBuff(loserBuff);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            if (winner == Define.Camp.Red)
            {
                _teamRedScore++;
            }
            else if (winner == Define.Camp.Blue)
            {
                _teamBlueScore++;
            }
            GetComponent<PhotonView>().RPC("SyncScore", RpcTarget.All, _teamRedScore, _teamBlueScore);
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
        character._playerStateMachine.ChangeState(character._playerStateMachine._playerIdleState);
        character.stat.CurHp = character.stat.MaxHp;
        character.BuffController.CancelUnSustainBuff();
        character.GetComponent<PhotonView>().RPC("SetHpRPC", RpcTarget.All, character.stat.CurHp);
        int layer = Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Red : (int)Define.Layer.Blue;
        character.GetComponent<PhotonView>().RPC("SetLayer", RpcTarget.All, layer);
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

    [PunRPC]
    public void SyncScore(int redScore, int blueScore)
    {
        TeamRedScore.text = $"{redScore}";
        TeamBlueScore.text = $"{blueScore}";
        _teamBlueScore = blueScore;
        _teamRedScore = redScore;
    }

    [PunRPC]
    public void SyncRound(int round)
    {
        roundTxt.text = round.ToString();
        currentRound = round;
    }
}