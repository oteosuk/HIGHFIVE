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
        skillData.skillName = "섬광";
        skillData.info = "적에게 피해를 가하면 출혈데미지를 입힌다.";
        skillData.skillSprite = Main.ResourceManager.Load<Sprite>("Sprites/SkillIcon/Flash");
        skillData.coolTime = 30;
        skillData.curTime = skillData.coolTime;
        skillData.isUse = true;
        skillData.skillRange = 3;
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
            myCharacter.transform.position = (Vector2)myCharacter.transform.position + (hit.distance * direction);
            return false;
        }
        return true;
    }
}
