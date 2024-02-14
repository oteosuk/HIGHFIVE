using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinationBuff : BaseBuff
{
    private BuffDBEntity _assassinationBuffData;
    protected override void Start()
    {
        base.Start();
        if (Main.DataManager.BuffDict.TryGetValue("암살", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        Debug.Log("sdffs");
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/arrow");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = 3;
        buffData.coolTimeicon = transform.Find("BuffCool").GetComponent<Image>();
        buffData.curTime = 0;
        buffData.trueDamage = 3;
        GetComponent<Image>().sprite = buffData.buffSprite;
        StartCoroutine(DealingPerSec());
        StartCoroutine(ActiveBuff());
    }
    protected override IEnumerator ActiveBuff()
    {
        yield return base.ActiveBuff();
    }

    private IEnumerator DealingPerSec()
    {
        while(buffData.curTime <= buffData.duration)
        {
            buffData.curTime += 1f;
            _stat.CurHp -= buffData.trueDamage;
            yield return new WaitForSeconds(1f);
        }
        buffData.curTime = 0;
    }

    protected override void LoseBuff()
    {
        base.LoseBuff();
    }

    public override void Refill()
    {
        base.Refill();
        StopCoroutine(ActiveBuff());
        buffData.curTime = 0;
        StartCoroutine(ActiveBuff());
    }
}
