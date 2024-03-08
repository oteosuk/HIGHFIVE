using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationData
{
    private string idleParmeterName = "isIdle";
    private string moveParmeterName = "isMove";
    private string AttackParmeterName = "isAttack";
    private string dieParameterName = "isDie";
    private string firstSkillParameterName = "isFirstSkill";
    private string secondSkillParameterName = "isSecondSkill";
    private string thirdSkillParameterName = "isThirdSkill";
    private string skillDelayParameterName = "isSkillDelay";
    private string counfuseParameterName = "isConfuse";
    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }
    public int FirstSkillParameterHash { get; private set; }
    public int SecondSkillParameterHash { get; private set; }
    public int ThirdSkillParameterHash { get; private set; }
    public int SkillDelayTimeHash { get; private set; }
    public int ConfuseParameterHash { get; private set; }
    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParmeterName);
        MoveParameterHash = Animator.StringToHash(moveParmeterName);
        AttackParameterHash = Animator.StringToHash(AttackParmeterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
        FirstSkillParameterHash = Animator.StringToHash(firstSkillParameterName);
        SecondSkillParameterHash = Animator.StringToHash(secondSkillParameterName);
        ThirdSkillParameterHash = Animator.StringToHash(thirdSkillParameterName);
        SkillDelayTimeHash = Animator.StringToHash(skillDelayParameterName);
        ConfuseParameterHash = Animator.StringToHash(counfuseParameterName);
    }
}