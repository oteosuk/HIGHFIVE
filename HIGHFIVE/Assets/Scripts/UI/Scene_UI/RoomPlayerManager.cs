using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerManager : RoomScene_UI
{
    private enum Images
    {
        ReadyImage
    }

    private Image _readyImage;
    private void Start()
    {
        Bind<Image>(typeof(Images));
        _readyImage = Get<Image>((int)Images.ReadyImage);
        _readyImage.color = Define.RedColor;
        UpdateCurrentRoomInfo();
    }

    //현재 방에 있는 플레이어들과 플레이어 수를 업데이트 해주는 함수
    private void UpdateCurrentRoomInfo()
    {
        _roomInfo.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

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
            if (Main.NetworkManager.photonRoomDict.TryGetValue(player.NickName, out bool isContain))
            {
                if (isContain) continue;
            }

            TMP_Text playerRole = gameObject.transform.Find("PlayerRole").GetComponent<TMP_Text>();
            TMP_Text playerName = gameObject.transform.Find("PlayerName").GetComponent<TMP_Text>();
            Image playerReadyImage = gameObject.transform.Find("ReadyImage").GetComponent<Image>();

            playerName.text = player.NickName;
            playerRole.text = player.IsMasterClient == true ? "[방장]" : "[게스트]";

            if (player.IsMasterClient) playerReadyImage.gameObject.SetActive(false);

            Main.NetworkManager.photonRoomDict[player.NickName] = true;
        }
    }

    //플레이어가 방에 들어왔을때 호출되는 함수 (본인제외)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    //플레이어가 방에 들어왔을때 호출되는 함수 (본인제외)
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Main.NetworkManager.photonRoomDict.ContainsKey(PhotonNetwork.NickName))
        {
            Main.NetworkManager.photonRoomDict[otherPlayer.NickName] = false;

            foreach (Transform child in _playerListContent.transform)
            {
                if (child.Find("PlayerName").GetComponent<TMP_Text>().text == otherPlayer.NickName)
                {
                    Destroy(child);
                    break;
                }
            }
        }
    }
}
