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
    }

    public override void Activation()
    {
        myCharacter.stat.Defence += _blueBuffData.def;
    }

    public override void Deactivation()
    {
        myCharacter.stat.Defence -= _blueBuffData.def;
    }
}