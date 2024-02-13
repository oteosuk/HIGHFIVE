using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public event Action<BaseSkill> skillExecuteEvent;
    public event Action<SkillData> skillDelayEvent;
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
}
