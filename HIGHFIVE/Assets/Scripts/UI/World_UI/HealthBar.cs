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

        if (transform.parent.tag == "Player")
        {
            if (transform.parent.gameObject.layer == (int)Define.Camp.Red)
            {
                Get<Image>((int)Images.Fill).color = Define.GreenColor;
            }
            else
            {
                Get<Image>((int)Images.Fill).color = Define.BlueColor;
            }
        }
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
