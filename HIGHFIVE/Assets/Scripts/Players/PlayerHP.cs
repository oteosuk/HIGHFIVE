using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : UIBase
{
    private Image _hpBar;
    private PlayerMove _playerMove;

    private enum Images
    {
        HpBar
    }

    private void Start()
    {
        Bind<GameObject>(typeof(Images), true);

        _hpBar = Get<GameObject>((int)Images.HpBar).GetComponentInChildren<Image>();
        _playerMove = GetComponent<PlayerMove>();

        // 콜라이더 아래 부착해주기 위함
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.down * (parent.GetComponent<Collider2D>().bounds.size.y - 0.1f);

        if (_playerMove != null)
        {
            _playerMove._damageTest += DamageTest;
        }
    }

    private void DamageTest(Collision2D colliision)
    {
        Debug.Log("구독");
        _hpBar.fillAmount -= 0.1f;
    }
}
