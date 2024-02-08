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
            transform.parent.GetComponent<PhotonView>().RPC("SetHpBarColor", RpcTarget.AllBuffered);
        }
        _statController = transform.parent.GetComponent<StatController>();
        _statController.hpChangeEvent += SetHpRatio; 
    }

    private void Update()
    {
        Transform parent = transform.parent;
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        rectTransform.position = new Vector2(parent.position.x, parent.position.y + parent.GetComponent<CapsuleCollider2D>().bounds.size.y);
    }

    private void SetHpRatio(int curHp, int maxHp)
    {
        float ratio = curHp / (float)maxHp;
        transform.parent.GetComponent<PhotonView>().RPC("SyncHpRatio", RpcTarget.AllBuffered, ratio);
    }
}
