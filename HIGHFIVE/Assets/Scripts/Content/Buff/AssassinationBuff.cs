using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinationBuff : BaseBuff
{
    private BuffDBEntity _assassinationBuffData;
    private float dealingTime;
    protected override void Start()
    {
        base.Start();
        if (Main.DataManager.BuffDict.TryGetValue("암살", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/arrow");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = 30;
        buffData.coolTimeicon = transform.Find("BuffCool").GetComponent<Image>();
        buffData.curTime = 0;
        dealingTime = 0;
        buffData.trueDamage = 3;
        GetComponent<Image>().sprite = buffData.buffSprite;
        myCharacter.GetComponent<StatController>().dieEvent += OffBuff;
        StartCoroutine(DealingPerSec());
        StartCoroutine(ActiveBuff());
    }
    protected override IEnumerator ActiveBuff()
    {
        yield return base.ActiveBuff();
    }

    private IEnumerator DealingPerSec()
    {
        while(dealingTime < buffData.duration)
        {
            dealingTime += 1f;
            _stat.TakeDamage(buffData.trueDamage, isTrueDamage: true);
            yield return new WaitForSeconds(1f);
        }
        dealingTime = 0;
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

    private void OffBuff()
    {
        buffData.curTime = buffData.duration;
        dealingTime = buffData.duration;
    }
}
