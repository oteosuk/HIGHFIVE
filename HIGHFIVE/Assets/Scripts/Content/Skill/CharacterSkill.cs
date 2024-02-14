using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    protected SkillController _skillController;
    public BaseSkill FirstSkill { get; protected set; }

    protected virtual void Start()
    {
        _skillController = GetComponent<SkillController>();
    }
}