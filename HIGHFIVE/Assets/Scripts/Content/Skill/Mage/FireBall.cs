using Photon.Pun;
using System;
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
        skillData.buffSprite = Main.ResourceManager.Load<Sprite>("Sprites/Projectile/MageNormal");
        skillData.coolTime = 5;
        skillData.curTime = 0;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = 300;
        skillData.damage = _fireBallData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _fireBallData.damageRatio);
    }
    public override void Execute()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        if (!skillData.isUse) return;

        skillData.isUse = false;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);

        InstantiateAfterLoad();
    }

    private async void InstantiateAfterLoad()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(350));
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 dir = GetDir().normalized;
        GameObject sphere = Main.ResourceManager.Instantiate("Character/FireBall", myCharacter.transform.position, syncRequired: true);
        SetRotation(sphere, dir);
        SetSpeed(sphere, dir);

        sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(myCharacter.gameObject);
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
        Vector2 point = Camera.main.ScreenToWorldPoint(Main.GameManager.SpawnedCharacter.Input._playerActions.Move.ReadValue<Vector2>());
        return (point - (Vector2)Main.GameManager.SpawnedCharacter.transform.position);
    }
}
