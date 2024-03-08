using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SkillScene_UI : MonoBehaviour
{
    private CharacterSkill _skill;
    public Dictionary<string, BaseSkill> Skills { get; private set; } = new Dictionary<string, BaseSkill>();
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _skill = Main.GameManager.SpawnedCharacter.CharacterSkill;
        BaseSkill firstSkill = _skill?.FirstSkill;
        BaseSkill secondSkill = _skill?.SecondSkill;
        BaseSkill thirdSkill = _skill?.ThirdSkill;

        if (firstSkill != null) CreateSkill(firstSkill);
        if (secondSkill != null) CreateSkill(secondSkill);
        if (thirdSkill != null) CreateSkill(thirdSkill);
    }

    private void CreateSkill(BaseSkill skill)
    {
        GameObject skillPrefab = Main.ResourceManager.Instantiate("UI_Prefabs/Skill", parent: transform,changingName:skill.skillData.skillName);
        Transform skillImage = skillPrefab.transform.Find("SkillImage");

        skill.skillData.skillPrefab = skillPrefab;
        skillImage.GetComponent<Image>().sprite = skill.skillData.skillSprite;
        skill.skillData.coolTimeicon = skillImage.Find("SkillCool").GetComponent<Image>();

        Skills.Add(skill.skillData.skillName, skill);
    }
}
