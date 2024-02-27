using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Flash : BaseSkill
{
    private SkillDBEntity _flashData;
    public override void Init()
    {
        base.Init();
        if (Main.DataManager.SkillDict.TryGetValue("섬광", out SkillDBEntity flashData))
        {
            _flashData = flashData;
        }
        //나중에 데이터 매니저에서 받아오기
        skillData.skillName = _flashData.name;
        skillData.info = "커서 방향으로 챔피언이 짧은 거리를 순간이동 합니다.";
        if (Main.GameManager.InGameObj.TryGetValue("FlashSprite", out Object obj)) { skillData.skillSprite = obj as Sprite; }
        else { skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/FlashSprite"); ; }
        skillData.coolTime = _flashData.coolTime;
        skillData.curTime = skillData.coolTime;
        skillData.isUse = true;
        skillData.skillRange = _flashData.range;
    }
    public override bool CanUseSkill()
    {
        if (!skillData.isUse) return false;
        return true;
    }

    public override void Execute()
    {
        skillData.isUse = false;
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 vector = GetDir();
        Vector2 direction = vector.normalized;
        float distance = vector.magnitude;
        myCharacter.NavMeshAgent.enabled = false;

        if (CheckArrive(direction))
        {
            if (distance < skillData.skillRange)
            {
                myCharacter.transform.position = (Vector2)myCharacter.transform.position + (vector);
            }
            else
            {
                Vector3 newPos = (Vector2)myCharacter.transform.position + (direction * skillData.skillRange);
                myCharacter.transform.position = newPos;
            }
        }

        if (Main.GameManager.InGameObj.TryGetValue("Flash", out Object obj)) { myCharacter.AudioSource.clip = obj as AudioClip; }
        else { myCharacter.AudioSource.clip = Main.ResourceManager.Load<AudioClip>("Sounds/SFX/InGame/Flash"); }
        myCharacter.GetComponent<PhotonView>().RPC("ShareEffectSound", RpcTarget.Others, "Flash");
        Main.SoundManager.PlayEffect(myCharacter.AudioSource);

        myCharacter.NavMeshAgent.enabled = true;
        myCharacter._playerStateMachine.ChangeState(myCharacter._playerStateMachine._playerIdleState);
        myCharacter.SkillController.CallSkillExecute(myCharacter.CharacterSkill.ThirdSkill);
        myCharacter.SkillController.CallSkillDelay(myCharacter.CharacterSkill.ThirdSkill.skillData);
    }

    private Vector2 GetDir()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        Vector2 mousePoint = myCharacter.Input._playerActions.Move.ReadValue<Vector2>();
        Vector2 raymousePoint = Camera.main.ScreenToWorldPoint(mousePoint);
        return raymousePoint - (Vector2)myCharacter.transform.position;
    }

    private bool CheckArrive(Vector2 direction)
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        int mask = 1 << (int)Define.Layer.Wall;
        RaycastHit2D hit = Physics2D.Raycast(myCharacter.transform.position, direction, skillData.skillRange, mask);
        if (hit.collider != null)
        {
            myCharacter.transform.position = (Vector2)myCharacter.transform.position + (hit.distance * direction) - direction.normalized;
            return false;
        }
        return true;
    }

    public override void RenewalInfo()
    {
        skillData.info = "커서 방향으로 챔피언이 짧은 거리를 순간이동 합니다.";
    }
}
