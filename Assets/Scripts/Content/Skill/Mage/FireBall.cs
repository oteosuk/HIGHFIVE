using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.TextCore.Text;

public class FireBall : BaseSkill
{
    private SkillDBEntity _fireBallData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("파이어볼", out SkillDBEntity firballData))
        {
            _fireBallData = firballData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = _fireBallData.name;
        skillData.info = $"커서 방향으로 화염포를 날려 적에게 " +
            $"{_fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio)}만큼의 피해를 입힌다.";
        if (Main.GameManager.InGameObj.TryGetValue("FireBall", out Object obj)) { skillData.skillSprite = obj as Sprite; }
        else { skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/FireBall"); }

        skillData.coolTime = _fireBallData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = _fireBallData.animTime;
        skillData.isUse = true;
        skillData.loadTime = _fireBallData.castingTime;
        skillData.damage = _fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio);
        skillData.skillRange = _fireBallData.range;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        return true;
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;

        skillData.isUse = false;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);

        InstantiateAfterLoad();
    }

    private void InstantiateAfterLoad()
    {
        Vector2 dir = GetDir().normalized;
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        GameObject sphere = Main.ResourceManager.Instantiate("SkillEffect/FireBallEffect", myCharacter.transform.position, syncRequired: true);
        SetRotation(sphere, dir);
        SetSpeed(sphere, dir);

        sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(myCharacter.gameObject);

        if (Main.GameManager.InGameObj.TryGetValue("MageQ", out Object obj)) { myCharacter.AudioSource.clip = obj as AudioClip; }
        else { myCharacter.AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/MageQ"); }
        myCharacter.GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "MageQ");
        Main.SoundManager.PlayEffect(myCharacter.AudioSource);
    }

    private void SetRotation(GameObject go, Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        go.transform.rotation = rotation;
    }

    private void SetSpeed(GameObject go, Vector2 dir)
    {
        Rigidbody2D rigidbody = go.GetComponent<Rigidbody2D>();
        rigidbody.velocity = dir.normalized * 10.0f;
    }

    private Vector2 GetDir()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Main.GameManager.SpawnedCharacter.MousePoint);
        return (point - (Vector2)Main.GameManager.SpawnedCharacter.transform.position);
    }

    public override void RenewalInfo()
    {
        skillData.info = $"스킬 사용 시 커서 방향으로 구체를 날리고 해당 구체에 맞은 적은 " +
            $"{_fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio)}만큼의 피해를 입힌다";
    }
}