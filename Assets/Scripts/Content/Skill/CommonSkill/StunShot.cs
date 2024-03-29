using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunShot : BaseSkill
{
    private SkillDBEntity _stunShotSkillData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("스턴샷", out SkillDBEntity stunShotSkillData))
        {
            _stunShotSkillData = stunShotSkillData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = _stunShotSkillData.name;
        skillData.info = "적에게 투사체를 던져 짧은 시간동안 기절시킨다.";
        if (Main.GameManager.InGameObj.TryGetValue("StunShotSprite", out Object obj)) { skillData.skillSprite = obj as Sprite; }
        else { skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/StunShotSprite"); }

        skillData.coolTime = _stunShotSkillData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = _stunShotSkillData.animTime;
        skillData.isUse = true;
        skillData.loadTime = _stunShotSkillData.castingTime;
        skillData.skillRange = _stunShotSkillData.range;
        skillData.damage = _stunShotSkillData.damage;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        if (myCharacter.targetObject?.layer == (int)Define.Layer.Monster)
        {
            return false;
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            SetTarget();
        }
        if (!CheckRange()) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        skillData.isUse = false;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.SecondSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.SecondSkill.skillData);
        InstantiateAfterLoad();
    }

    private void InstantiateAfterLoad()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Main.ResourceManager.Instantiate("SkillEffect/StunShot", myCharacter.transform.position, syncRequired: true);

        if (Main.GameManager.InGameObj.TryGetValue("StunShot", out Object obj)) { myCharacter.AudioSource.clip = obj as AudioClip; }
        else { myCharacter.AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/StunShot"); }
        myCharacter.GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "StunShot");
        Main.SoundManager.PlayEffect(myCharacter.AudioSource);
    }

    private bool CheckRange()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;

        if (myCharacter.targetObject != null)
        {
            float distance = (myCharacter.targetObject.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange >= distance) { return true; }
            else { return false; }
        }
        else
        {
            return false;
        }
    }

    private void SetTarget()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 mousePoint = myCharacter.MousePoint;
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

        if (hit.collider?.gameObject != null)
        {
            myCharacter.targetObject = hit.collider.gameObject;
            _targetObject = myCharacter.targetObject;
            float distance = (hit.collider.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange < distance)
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine._playerMoveState);
            }
        }
    }

    public override void RenewalInfo()
    {
        skillData.info = "해당 챔피언에게 투사체를 던지고 투사체에 맞은 챔피언은 기절하게 됩니다.";
    }
}
