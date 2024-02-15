using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public struct BuffData
{
    public Type type;
    public float duration;
    public float effectTime;
    public float curTime;
    public int trueDamage;
    public int damage;
    public bool isSustainBuff;
    public Image coolTimeicon;
    public Sprite buffSprite;
}

public abstract class BaseBuff
{
    public BuffData buffData;
    protected BuffController _buffController;
    protected Stat _stat;
    protected Character myCharacter;

    public virtual void Init() { }
    public virtual IEnumerator ApplyEffect(GameObject target) { yield return null; }
    public abstract void Activation();

    public abstract void Deactivation();
}
