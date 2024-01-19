using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectScene : BaseScene
{
    protected override void Init()
    {
        base.Init(); //공통적으로 실행하는 부분
                     // 추가 코드 -> 이 씬에서만 따로 또 실행해햐하는 메서드
        int index = 0;

        Player[] currentRoomPlayer = PhotonNetwork.PlayerList;
        foreach (Player player in currentRoomPlayer)
        {
            if (player.NickName == PhotonNetwork.NickName)
            {
                if (index % 2 == 0)
                {
                    Main.GameManager.SelectedCamp = Define.Camp.Red;
                }
                else
                {
                    Main.GameManager.SelectedCamp = Define.Camp.Blue;
                }
            }
            index++;
        }
    }
}
