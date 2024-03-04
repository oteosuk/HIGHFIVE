using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTeamColor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        TeamColor();
    }

    private void TeamColor()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        PhotonView pv = myCharacter.GetComponent<PhotonView>();

        if (pv.IsMine)
        {
            _spriteRenderer.color = Define.GreenColor;
            pv.RPC("SyncMiniMapColor", RpcTarget.OthersBuffered);
        }
    }
}