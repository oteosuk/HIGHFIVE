using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListController : UIBase
{
    private enum Texts
    {
        RoomInfo
    }
    private enum GameObjects
    {
        PlayerListContent
    }

    private TMP_Text _roomInfo;
    private GameObject _playerListContent;
    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts), true);
        Bind<GameObject>(typeof(GameObjects), true);

        _roomInfo = Get<TMP_Text>((int)Texts.RoomInfo);
        _playerListContent = Get<GameObject>((int)GameObjects.PlayerListContent);

        UpdatePlayerList();
    }

    //플레이어가 방에 들어왔을때 호출되는 함수 (본인제외)
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    //플레이어가 방에 나갈 때 호출되는 함수 (본인제외)
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (Main.NetworkManager.photonPlayerDict.ContainsKey(otherPlayer.NickName))
        {
            Main.NetworkManager.photonPlayerDict[otherPlayer.NickName] = null;
            Main.NetworkManager.photonReadyImageDict.Remove(otherPlayer.NickName);
            foreach (Transform child in _playerListContent.GetComponent<RectTransform>())
            {
                if (child.Find("PlayerName").GetComponent<TMP_Text>().text == otherPlayer.NickName)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
        }

        UpdateRoomInfo();
    }

    //플레이어가 방에 참가 했을때 플레이어 닉네임을 key로 하는  Dict하나 생성
    //플레이어가 방에 나갔을 때 해당 플레이어의 닉네임으로 값을가져와서 수정
    private void UpdatePlayerList()
    {
        UpdateRoomInfo();

         Player[] players = PhotonNetwork.PlayerList;

        // 각 플레이어의 정보 활용
        foreach (Player player in players)
        {
            //내 로컬 상에 있는 딕셔너리 데이터가 true라면 continue;
            if (Main.NetworkManager.photonPlayerDict.TryGetValue(player.NickName, out Player p1))
            {
                if (p1 != null) continue;
            }

            GameObject newPlayer = Main.ResourceManager.Instantiate("UI_Prefabs/Player", _playerListContent.transform, $"{player.NickName}Player");
            TMP_Text playerRole = newPlayer.transform.Find("PlayerRole").GetComponent<TMP_Text>();
            TMP_Text playerName = newPlayer.transform.Find("PlayerName").GetComponent<TMP_Text>();
            Image playerReadyImage = newPlayer.transform.Find("ReadyImage").GetComponent<Image>();

            if (!Main.NetworkManager.photonReadyImageDict.ContainsKey(player.NickName))
            {
                Main.NetworkManager.photonReadyImageDict.Add(player.NickName, playerReadyImage);
            }
            

            playerName.text = player.NickName;
            playerRole.text = player.IsMasterClient == true ? "[방장]" : "[게스트]";
            playerReadyImage.color = Define.RedColor;

            if (player.IsMasterClient) playerReadyImage.gameObject.SetActive(false);

            Main.NetworkManager.photonPlayerDict[player.NickName] = player;
        }
    }

    private void UpdateRoomInfo()
    {
        _roomInfo.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";
    }

}
