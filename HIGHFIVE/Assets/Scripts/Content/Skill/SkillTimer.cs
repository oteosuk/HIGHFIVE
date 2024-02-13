using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimer : MonoBehaviour
{
    private SkillController _skillController;
    private void Start()
    {
        _skillController = GetComponent<SkillController>();
        _skillController.skillExecuteEvent += StartCoolDown;
        _skillController.skillDelayEvent += StarSkillDelay;
    }

    private void StartCoolDown(BaseSkill skill)
    {
        StartCoroutine(StartCoolCoroutine(skill));
    }

    private void StarSkillDelay(SkillData skillData)
    {
        StartCoroutine(StartSkillDelayCoroutine(skillData));
    }

    IEnumerator StartCoolCoroutine(BaseSkill skill)
    {
        //while (skillData.curTime > 0)
        //{
        //    skillData.curTime -= 0.1f;
        //    skillData.coolTimeicon.fillAmount = skillData.curTime / skillData.coolTime;
        //    yield return new WaitForSeconds(0.1f);
        //}
        //skillData.coolTimeicon.fillAmount = 0;
        //skillData.curTime = 0;
        yield return new WaitForSeconds(2f);
        skill.skillData.isUse = true;
    }
    IEnumerator StartSkillDelayCoroutine(SkillData skillData)
    {
        yield return new WaitForSeconds(skillData.animTime);
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        myCharacter.Animator.SetBool(myCharacter.PlayerAnimationData.SkillDelayTimeHash, false);
    }
}
