using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinationBuff : BaseBuff
{
    private BuffDBEntity _assassinationBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("암살", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/arrow");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = 30;
        buffData.curTime = 0;
        buffData.effectTime = 0;
        buffData.trueDamage = 3;
        buffData.isSustainBuff = false;
    }

    public override void Activation() { }

    public override void Deactivation() { }

    public override IEnumerator ApplyEffect(GameObject target)
    {
        yield return base.ApplyEffect(target);

        while (buffData.effectTime < buffData.duration)
        {
            buffData.effectTime += 1f;
            target.GetComponent<Stat>().TakeDamage(buffData.trueDamage, isTrueDamage: true);
            yield return new WaitForSeconds(1f);
        }
        buffData.effectTime = 0;
    }
}
