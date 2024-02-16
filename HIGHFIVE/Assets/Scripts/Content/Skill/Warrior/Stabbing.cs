using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stabbing : BaseSkill
{
    private SkillDBEntity _stabbingData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("찌르기", out SkillDBEntity stabbingData))
        {
            _stabbingData = stabbingData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/MageNormal");
        skillData.coolTime = 5;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = 0;
        skillData.durationTime = 5;
        skillData.skillRange = 1;
        //_assassinationData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _assassinationData.damageRatio);
        skillData.damage = 20;
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
