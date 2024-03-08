using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTeamColor : MonoBehaviour
{
    private PhotonView _pv;
    void Start()
    {
        //나의 렌더러만 가져와서 그린
        _pv = gameObject.GetComponent<PhotonView>();
        TeamColor();
    }

    private void TeamColor()
    {
        if (_pv.IsMine)
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = Define.GreenColor;
            _pv.RPC("SyncMiniMapColor", RpcTarget.OthersBuffered);
        }
    }
}