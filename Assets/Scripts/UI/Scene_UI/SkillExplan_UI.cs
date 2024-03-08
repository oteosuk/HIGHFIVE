using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillExplan_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _info;
    public TMP_Text _skillName;
    public TMP_Text _skillInfo;
    private SkillScene_UI _skillUI;

    private void Start()
    {
        _skillUI = transform.parent.GetComponent<SkillScene_UI>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _info.SetActive(true);
        
        GameObject targetObject = eventData.pointerEnter;

        if (_skillUI.Skills.TryGetValue(targetObject.name, out BaseSkill skill))
        {
            skill.RenewalInfo();
            _skillName.text = skill.skillData.skillName;
            _skillInfo.text = skill.skillData.info;
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _info.SetActive(false);
    }

}
