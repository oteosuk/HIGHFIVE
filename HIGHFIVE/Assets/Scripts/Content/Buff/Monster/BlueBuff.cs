using UnityEngine;

public class BlueBuff : BaseBuff
{
    private BuffDBEntity _blueBuffData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.BuffDict.TryGetValue("블루", out BuffDBEntity blueBuffData))
        {
            _blueBuffData = blueBuffData;
        }
        //나중에 데이터 매니저에서 받아오기
        buffData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/BuffIcon/Blue");
        buffData.type = typeof(BlueBuff);
        buffData.duration = _blueBuffData.durationTime;
        buffData.curTime = 0;
        buffData.isSustainBuff = _blueBuffData.isSustain; //유지여부
        buffData.buffName = "블루";
        buffData.info = $"자신의 DEF를 {_blueBuffData.def}만큼 올려줍니다.";
    }

    public override void Activation()
    {
        myCharacter.stat.Defence += _blueBuffData.def;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Defence -= _blueBuffData.def;
    }

    public override void RenewalInfo()
    {
        buffData.info = $"자신의 방어력을 {_blueBuffData.def}만큼 올려줍니다.";
    }
}