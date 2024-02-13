using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkills : CharacterSkill
{
    private void Start()
    {
        FirstSkill = new FireBall();
        FirstSkill.Init();
    }

    //public void ChangeSkill(BaseSkill skill, Define.SkillNum skillNum)
    //{
    //    skill1 = skill;
    //    skill1.Init();
    //}
}
