using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SkillData
{
    public float coolTime;
    public float curTime;
    public float animTime;
    public int damage;
    public double loadTime;
    public bool isUse;
    public Image coolTimeicon;
    public Sprite buffSprite;
}

public abstract class BaseSkill
{
    public SkillData skillData;
    public virtual void Init() { }

    public abstract void Execute();
}
