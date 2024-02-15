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


    public void OnPointerEnter(PointerEventData eventData)
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _info.SetActive(true);
        Debug.Log(myCharacter.CharacterSkill.FirstSkill.skillData.skillName);
        _skillName.text = myCharacter.CharacterSkill.FirstSkill.skillData.skillName;
        _skillInfo.text = myCharacter.CharacterSkill.FirstSkill.skillData.info;
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _info.SetActive(false);
    }

}
