using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StabbingBuff : BaseBuff
{
    private BuffDBEntity _stabbingBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("찌르기", out BuffDBEntity stabbingBuffData))
        {
            _stabbingBuffData = stabbingBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/MageNormal");
        buffData.type = typeof(StabbingBuff);
        buffData.duration = 10;
        buffData.curTime = 0;
        buffData.damage = 20;
        buffData.effectTime = 2;
    }
    public override IEnumerator ApplyEffect(GameObject target)
    {
        yield return new WaitForSeconds(buffData.effectTime);
        target.GetComponent<Stat>().MoveSpeed -= 1;
    }

    public override void Activation()
    {
        _stat.MoveSpeed += 1;
    }

    public override void Deactivation() { }
}
