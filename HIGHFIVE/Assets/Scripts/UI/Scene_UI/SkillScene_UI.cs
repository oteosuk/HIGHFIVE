using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScene_UI : MonoBehaviour
{
    private CharacterSkill _skill;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _skill = Main.GameManager.SpawnedCharacter.CharacterSkill;
        BaseSkill firstSkill = _skill?.FirstSkill;
        BaseSkill secondSkill = _skill?.SecondSkill;
        if (firstSkill != null) CreateSkill(firstSkill);
        if (secondSkill != null) CreateSkill(secondSkill);
    }

    private void CreateSkill(BaseSkill skill)
    {
        GameObject skillPrefab = Main.ResourceManager.Instantiate("UI_Prefabs/Skill", parent: transform);
        Transform skillImage = skillPrefab.transform.Find("SkillImage");

        skill.skillData.skillPrefab = skillPrefab;
        skillImage.GetComponent<Image>().sprite = skill.skillData.skillSprite;
        skill.skillData.coolTimeicon = skillImage.Find("SkillCool").GetComponent<Image>();
    }
}
