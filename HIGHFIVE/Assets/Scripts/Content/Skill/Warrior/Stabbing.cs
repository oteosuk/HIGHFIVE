using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stabbing : BaseSkill
{
    private SkillDBEntity _stabbingData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("광폭화", out SkillDBEntity stabbingData))
        {
            _stabbingData = stabbingData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Berserk");
        skillData.coolTime = _stabbingData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = 0;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        if (!skillData.isUse) return;
        skillData.isUse = false;
        skillData.coolTimeicon.fillAmount = 1;
        BaseBuff stabbingBuff = new StabbingBuff();
        myCharacter.BuffController.AddBuff(stabbingBuff);
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);
    }
}
