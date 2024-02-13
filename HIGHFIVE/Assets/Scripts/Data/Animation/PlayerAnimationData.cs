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
    private string skillDelayParameterName = "isSkillDelay";
    public int IdleParameterHash { get; private set; }
    public int MoveParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int DieParameterHash { get; private set; }
    public int FirstSkillParameterHash { get; private set; }
    public int SkillDelayTimeHash { get; private set; }
    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParmeterName);
        MoveParameterHash = Animator.StringToHash(moveParmeterName);
        AttackParameterHash = Animator.StringToHash(AttackParmeterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
        FirstSkillParameterHash = Animator.StringToHash(firstSkillParameterName);
        SkillDelayTimeHash = Animator.StringToHash(skillDelayParameterName);
    }
}