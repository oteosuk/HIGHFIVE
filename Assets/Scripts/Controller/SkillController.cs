using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public event Action<BaseSkill> skillExecuteEvent;
    public event Action<SkillData> skillDelayEvent;
    public event Action<SkillData> skillDuringEvent;
    public void CallSkillExecute(BaseSkill skill)
    {
        if (skillExecuteEvent != null)
        {
            skillExecuteEvent.Invoke(skill);
        }
    }
    public void CallSkillDelay(SkillData skillData)
    {
        if (skillDelayEvent != null)
        {
            skillDelayEvent.Invoke(skillData);
        }
    }
    public void CallSkillDuring(SkillData skillData)
    {
        if (skillDuringEvent != null)
        {
            skillDuringEvent.Invoke(skillData);
        }
    }
}
