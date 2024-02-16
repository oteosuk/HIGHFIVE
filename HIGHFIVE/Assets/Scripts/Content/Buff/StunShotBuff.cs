using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunShotBuff : BaseBuff
{
    private BuffDBEntity _assassinationBuffData;

    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("스턴샷", out BuffDBEntity assassinationBuffData))
        {
            _assassinationBuffData = assassinationBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/StunShot");
        buffData.type = typeof(AssassinationBuff);
        buffData.duration = 4;
        buffData.curTime = 0;
        buffData.effectTime = 0;
        buffData.isSustainBuff = false;
    }

    public override void Activation() 
    {

    }

    public override void Deactivation()
    {
        buffData.effectTime = buffData.duration;
    }

    public override IEnumerator ApplyEffect(GameObject target)
    {
        yield return base.ApplyEffect(target);
        target.GetComponent<Character>()._playerStateMachine.ChangeState(target.GetComponent<Character>()._playerStateMachine.PlayerConfuseState);
        yield return new WaitForSeconds(buffData.duration);
        target.GetComponent<Character>()._playerStateMachine.ChangeState(target.GetComponent<Character>()._playerStateMachine._playerIdleState);
        buffData.effectTime = 0;
    }
}
