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
                    if (Main.NetworkManager.photonPlayerDict.TryGetValue(player.NickName, out Player myPlayer))
                    {
                        if (myPlayer.CustomProperties.ContainsKey("layer"))
                        {
                            myPlayer.CustomProperties["layer"] = (int)Define.Layer.Red;
                        }
                        else
                        {
                            myPlayer.CustomProperties.Add("layer", (int)Define.Layer.Red);
                        }

                    }
                    Main.GameManager.SelectedCamp = Define.Camp.Red;
                }
                else
                {
                    if (Main.NetworkManager.photonPlayerDict.TryGetValue(player.NickName, out Player myPlayer))
                    {
                        if (myPlayer.CustomProperties.ContainsKey("layer"))
                        {
                            myPlayer.CustomProperties["layer"] = (int)Define.Layer.Blue;
                        }
                        else
                        {
                            myPlayer.CustomProperties.Add("layer", (int)Define.Layer.Blue);
                        }
                    }
                    Main.GameManager.SelectedCamp = Define.Camp.Blue;
                }
            }
            index++;
        }

    }
}
