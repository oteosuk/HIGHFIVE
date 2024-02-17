using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunShot : BaseSkill
{
    private SkillDBEntity _assassinationData;
    private GameObject _targetObject;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("스턴샷", out SkillDBEntity assassinationData))
        {
            _assassinationData = assassinationData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = "스턴샷";
        skillData.info = "적에게 피해를 가하면 출혈데미지를 입힌다.";
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/StunShot");
        skillData.coolTime = 5;
        skillData.curTime = skillData.coolTime;
        skillData.animTime = 0.5f;
        skillData.isUse = true;
        skillData.loadTime = 0;
        skillData.durationTime = 5;
        skillData.skillRange = 3;
        //_assassinationData.damage + (int)(Main.GameManager.SpawnedCharacter.stat.Attack * _assassinationData.damageRatio);
        skillData.damage = 20;
    }

    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
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

    private async void InstantiateAfterLoad()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 dir = myCharacter.targetObject.transform.position - myCharacter.transform.position;
        await Task.Delay(TimeSpan.FromMilliseconds(200));
        GameObject sphere = Main.ResourceManager.Instantiate("Character/StunShot", myCharacter.transform.position, syncRequired: true);
        PhotonView targetPhotonView = myCharacter.targetObject?.GetComponent<PhotonView>();

        sphere.GetComponent<PhotonView>().RPC("SetTarget", RpcTarget.All, targetPhotonView.ViewID);
        sphere.GetComponent<PhotonView>().RPC("ToTarget", RpcTarget.All, 5.0f, dir.x, dir.y);
        sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(myCharacter.gameObject);
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

        int mask = (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));

        RaycastHit2D hit = Physics2D.Raycast(raymousePoint, Camera.main.transform.forward, 10.0f, mask);

        if (hit.collider?.gameObject != null)
        {
            myCharacter.targetObject = hit.collider.gameObject;
            _targetObject = myCharacter.targetObject;
            float distance = (hit.collider.transform.position - myCharacter.transform.position).magnitude;

            if (skillData.skillRange >= distance)
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine.PlayerSecondSkillState);
            }
            else
            {
                myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine._playerMoveState);
            }
        }
        else
        {
            myCharacter.targetObject = null;
            _targetObject = null;
        }
    }
}
