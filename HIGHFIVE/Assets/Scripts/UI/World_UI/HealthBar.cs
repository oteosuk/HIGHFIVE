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
    private float originLocalScaleX;

    private void Start()
    {
        Bind<Image>(typeof(Images), true);

        if (transform.parent.tag == "Player")
        {
            transform.parent.GetComponent<PhotonView>().RPC("SetHpBarColor", RpcTarget.AllBuffered);
        }
        _statController = transform.parent.GetComponent<StatController>();
        _statController.hpChangeEvent += SetHpRatio;

        originLocalScaleX = transform.localScale.x;
    }

    private void Update()
    {
        Transform parent = transform.parent;
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        rectTransform.position = new Vector2(parent.position.x, parent.position.y + parent.GetComponent<CapsuleCollider2D>().bounds.size.y);
    }

    private void LateUpdate()
    {
        Transform parent = transform.parent;
        if (parent.localScale.x < 0)
        {
            transform.localScale = new Vector2(-originLocalScaleX, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(originLocalScaleX, transform.localScale.y);
        }
    }

    private void SetHpRatio(int curHp, int maxHp)
    {
        float ratio = curHp / (float)maxHp;
        if (ratio <= 0) return;
        transform.parent.GetComponent<PhotonView>().RPC("SyncHpRatio", RpcTarget.All, ratio);
    }
}
