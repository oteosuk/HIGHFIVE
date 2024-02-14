using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinationTimer : SkillTimer
{
    protected override void Start()
    {
        base.Start();
        _skillController.skillDuringEvent += StarSkillDuration;
    }
    private void StarSkillDuration(SkillData skillData)
    {
        StartCoroutine(StartSkillDurationCoroutine(skillData));
    }
    IEnumerator StartSkillDurationCoroutine(SkillData skillData)
    {
        float curTime = skillData.durationTime;
        while (curTime > 0)
        {
            curTime -= 0.1f;
            skillData.coolTimeicon.fillAmount = curTime / skillData.coolTime;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
