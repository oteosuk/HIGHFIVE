using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIBase
{
    private enum Images
    {
        Fill
    }

    private StatController _statController;

    private void Start()
    {
        Bind<Image>(typeof(Images), true);
        Get<Image>((int)Images.Fill).color = Define.GreenColor;
        //상대방의 LAYER를 확인해서 LAYER가 나와 같다면 GREEN
        //아니라면 RED  
        //transform.parent.GetComponent<PhotonView>().RPC("SyncHpBarColor", RpcTarget.All);

        //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    if (player.layer == 1 << (int)Main.GameManager.SelectedCamp)
        //    {
        //        player.GetComponentInChildren
        //    }
        //}
        _statController = transform.parent.GetComponent<StatController>();
        _statController.hpChangeEvent += SetHpRatio;
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = new Vector2(parent.position.x, parent.position.y);
        
    }

    private void SetHpRatio(int curHp, int maxHp)
    {
        float ratio = curHp / (float)maxHp;
        transform.parent.GetComponent<PhotonView>().RPC("SyncHpRatio", RpcTarget.All, ratio);
    }
}
