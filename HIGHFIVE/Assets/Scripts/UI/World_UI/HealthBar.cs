using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIBase
{
    private enum Images
    {
        Fill
    }
    private enum GameObjects
    {
        CharacterInfo,
        Level,
        Nickname
    }

    private StatController _statController;
    private float originLocalScaleX;
    private GameObject _levelObj;
    private GameObject _nameObj;
    private GameObject _characterInfoObj;

    private void Start()
    {
        Bind<Image>(typeof(Images), true);
        Bind<GameObject>(typeof(GameObjects), true);

        _levelObj = Get<GameObject>((int)GameObjects.Level);
        _nameObj = Get<GameObject>((int)GameObjects.Nickname);
        _characterInfoObj = Get<GameObject>((int)GameObjects.CharacterInfo);

        if (transform.parent.tag == "Player")
        {
            GameObject playerObj = transform.parent.gameObject;
            PhotonView pv = playerObj.GetComponent<PhotonView>();
            _characterInfoObj.SetActive(true);
            pv.RPC("SetHpBarColor", RpcTarget.AllBuffered);

            if (Main.NetworkManager.photonPlayer.TryGetValue(pv.ViewID, out Player player))
            {
                int level = playerObj.GetComponent<CharacterStat>().Level;

                Transform characterInfoObj = playerObj.transform.Find("HealthCanvas").Find("CharacterInfo");
                characterInfoObj.Find("Nickname").GetComponent<TMP_Text>().text = player.NickName;
                characterInfoObj.Find("Level").GetComponent<TMP_Text>().text = $"{level}Lv";
            }
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
        if (transform.parent.GetComponent<Character>() == null)
        {
            if (ratio <= 0) return;
        }


        transform.GetComponentInChildren<Slider>().value = ratio;
    }
}