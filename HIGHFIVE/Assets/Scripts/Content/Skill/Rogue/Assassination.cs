using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Assassination : BaseSkill
{
    private SkillDBEntity _assassinationData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("암살", out SkillDBEntity assassinationData))
        {
            _assassinationData = assassinationData;
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
        if (Keyboard.current.qKey.wasPressedThisFrame)
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
        DamageToTarget(_targetObject, myCharacter);
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, true);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.FirstSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.FirstSkill.skillData);
    }

    private void DamageToTarget(GameObject target, Character shooter)
    {
        target.GetComponent<Stat>().TakeDamage(skillData.damage, shooter.gameObject);
        BaseBuff assassinationBuff = new AssassinationBuff();
        target.GetComponent<Creature>().BuffController?.AddBuff(assassinationBuff);
        PhotonView targetPv = target.GetComponent<PhotonView>();
        if (Main.NetworkManager.photonPlayer.TryGetValue(targetPv.ViewID, out Player targetPlayer))
        {
            shooter.GetComponent<PhotonView>().RPC("ReceiveBuff", RpcTarget.Others, targetPv.ViewID, Define.Buff.Assassination);
        }
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
        Vector2 mousePoint = myCharacter.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

        if (hit.collider?.gameObject != null)
        {
            myCharacter.targetObject = hit.collider.gameObject;
            _targetObject = myCharacter.targetObject;
            float distance = (hit.collider.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange >= distance)
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine.PlayerFirstSkillState);
            }
            else
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine._playerMoveState);
            }
        }
    }
}
