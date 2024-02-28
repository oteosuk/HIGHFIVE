using UnityEngine;

public class LoserBuff : BaseBuff
{
    private BuffDBEntity _loserBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("패자의 분노", out BuffDBEntity loserBuffData))
        {
            _loserBuffData = loserBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/BuffIcon/LoserAnger");
        buffData.type = typeof(LoserBuff);
        buffData.duration = _loserBuffData.durationTime;
        buffData.curTime = 0;
        buffData.isSustainBuff = _loserBuffData.isSustain;
        buffData.buffName = "패자의 분노";
        buffData.info = $"배틀에서 진 자에게 ATK {_loserBuffData.atk} DEF {_loserBuffData.def} " +
            $"ATS {_loserBuffData.atkSpd} 를 부여합니다";
    }

    public override void Activation()
    {
        myCharacter.stat.Attack += _loserBuffData.atk;
        myCharacter.stat.Defence += _loserBuffData.def;
        myCharacter.stat.AttackSpeed += _loserBuffData.atkSpd;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Attack -= _loserBuffData.atk;
        myCharacter.stat.Defence -= _loserBuffData.def;
        myCharacter.stat.AttackSpeed -= _loserBuffData.atkSpd;
    }

    public override void RenewalInfo()
    {
        buffData.info = $"배틀에서 진 자에게 ATK {_loserBuffData.atk} DEF {_loserBuffData.def} " +
            $"ATS {_loserBuffData.atkSpd} 를 부여합니다";
    }
}