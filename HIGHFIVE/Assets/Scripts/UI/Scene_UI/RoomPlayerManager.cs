using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerManager : RoomScene_UI
{
    private void Start()
    {
        UpdatePlayerList();
    }

    //현재 방에 있는 플레이어들과 플레이어 수를 업데이트 해주는 함수
    private void UpdateCurrentRoomInfo()
    {
        
    }
    //플레이어가 방에 들어왔을때 호출되는 함수 (본인제외)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    //플레이어가 방에 참가 했을때 플레이어 닉네임을 key로 하는  Dict하나 생성
    //플레이어가 방에 나갔을 때 해당 플레이어의 닉네임으로 값을가져와서 수정
    private void UpdatePlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        // 각 플레이어의 정보 활용
        foreach (Player player in players)
        {
            //내 로컬 상에 있는 딕셔너리 데이터가 true라면 continue;
            if (Main.NetworkManager.photonPlayerDict.TryGetValue(player.NickName, out bool isContain))
            {
                if (isContain) continue;
            }

            GameObject newPlayer = Main.ResourceManager.Instantiate("UI_Prefabs/Player", transform);
            TMP_Text playerRole = newPlayer.transform.Find("PlayerRole").GetComponent<TMP_Text>();
            TMP_Text playerName = newPlayer.transform.Find("PlayerName").GetComponent<TMP_Text>();
            Image playerReadyImage = newPlayer.transform.Find("ReadyImage").GetComponent<Image>();

            playerName.text = player.NickName;
            playerRole.text = player.IsMasterClient == true ? "[방장]" : "[게스트]";
            playerReadyImage.color = Define.RedColor;

            if (player.IsMasterClient) playerReadyImage.gameObject.SetActive(false);

            Main.NetworkManager.photonPlayerDict[player.NickName] = true;
        }
    }

    
    
}
