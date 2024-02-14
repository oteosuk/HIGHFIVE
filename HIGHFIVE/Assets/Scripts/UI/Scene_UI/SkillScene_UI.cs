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
        Debug.Log(firstSkill);
        if (firstSkill != null )
        {
            GameObject skillPrefab = Main.ResourceManager.Instantiate("UI_Prefabs/Skill", parent: transform);
            Transform skillImage = skillPrefab.transform.Find("SkillImage");

            firstSkill.skillData.skillPrefab = skillPrefab;
            skillImage.GetComponent<Image>().sprite = firstSkill.skillData.skillSprite;
            firstSkill.skillData.coolTimeicon = skillImage.Find("SkillCool").GetComponent<Image>();
        }
    }
}
