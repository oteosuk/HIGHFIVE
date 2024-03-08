using UnityEngine;

public class EliteBuff : BaseBuff
{
    private BuffDBEntity _eliteBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("엘리트", out BuffDBEntity eliteBuffData))
        {
            _eliteBuffData = eliteBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        if (Main.GameManager.InGameObj.TryGetValue("Elite", out Object obj)) { buffData.buffSprite = obj as Sprite; }
        else { buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/BuffIcon/Elite"); }

        buffData.type = typeof(EliteBuff);
        buffData.duration = _eliteBuffData.durationTime;
        buffData.curTime = 0;
        buffData.isSustainBuff = _eliteBuffData.isSustain; //유지여부
        buffData.buffName = "불멸의 힘";
        buffData.info = $"자신의 ATK {_eliteBuffData.atk} DEF {_eliteBuffData.def} SPD {_eliteBuffData.moveSpd}" +
            $"만큼 올려줍니다.";
    }

    public override void Activation()
    {
        myCharacter.stat.Attack += _eliteBuffData.atk;
        myCharacter.stat.Defence += _eliteBuffData.def;
        myCharacter.stat.MoveSpeed += _eliteBuffData.moveSpd;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Attack -= _eliteBuffData.atk;
        myCharacter.stat.Defence -= _eliteBuffData.def;
        myCharacter.stat.MoveSpeed -= _eliteBuffData.moveSpd;
    }
    public override void RenewalInfo()
    {
        buffData.info = $"자신의 ATK {_eliteBuffData.atk} DEF {_eliteBuffData.def} SPD {_eliteBuffData.moveSpd}" +
            $"만큼 올려줍니다.";
    }
}