using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoserBuff : BaseBuff
{
    private BuffDBEntity _loserBuffData;
    protected override void Start()
    {
        base.Start();
        if (Main.DataManager.BuffDict.TryGetValue("패자의 분노", out BuffDBEntity loserBuffData))
        {
            _loserBuffData = loserBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.sprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/MageNormal");
        buffData.type = typeof(LoserBuff);
        buffData.duration = 60;
        buffData.icon = GetComponent<Image>();
        buffData.curTime = 60;
        _stat.Attack += _loserBuffData.atk;
        GetComponent<Image>().sprite = buffData.sprite;
        StartCoroutine(ActiveBuff());
    }
    protected override IEnumerator ActiveBuff()
    {
        yield return base.ActiveBuff();
    }

    protected override void LoseBuff()
    {
        _stat.Attack -= _loserBuffData.atk;
    }

    public override void Reset()
    {
        base.Reset();
        StopCoroutine(ActiveBuff());
        buffData.curTime = 60;
        StartCoroutine(ActiveBuff());
    }
}
