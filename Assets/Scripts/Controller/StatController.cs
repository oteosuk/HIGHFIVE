using System;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public event Action<int> attackChangeEvent;
    public event Action<int> defenceChangeEvent;
    public event Action<int> levelChangeEvent;
    public event Action<float> attackSpeedEvent; 
    public event Action<float> moveSpeedChangeEvent;
    public event Action<int,int> expChangeEvent;
    public event Action<int,int> hpChangeEvent;
    public event Action dieEvent;
    public event Action<GameObject> buffEvent;

    public void CallAddBuffEvent(GameObject gameobject)
    {
        if (buffEvent != null)
        {
            buffEvent.Invoke(gameobject);
        }
    }

    public void CallChangeHpEvent(int curHp, int maxHp)
    {
        if (hpChangeEvent != null)
        {
            hpChangeEvent.Invoke(curHp, maxHp);
        }
    }
    public void CallChangeExpEvent(int exp, int maxExp)
    {
        if (expChangeEvent != null)
        {
            expChangeEvent.Invoke(exp, maxExp);
        }
    }
    public void CallChangeAttackEvent(int attack)
    {
        if (attackChangeEvent != null)
        {
            attackChangeEvent.Invoke(attack);
        }
    }
    public void CallChangeDefenceEvent(int defence)
    {
        if (defenceChangeEvent != null)
        {
            defenceChangeEvent.Invoke(defence);
        }
    }
    public void CallChangeLevelEvent(int level)
    {
        if (levelChangeEvent != null)
        {
            levelChangeEvent.Invoke(level);
        }
    }
    public void CallChangeMoveSpeedEvent(float speed)
    {
        if (moveSpeedChangeEvent != null)
        {
            moveSpeedChangeEvent.Invoke(speed);
        }
    }
    public void CallDieEvent()
    {
        if (dieEvent != null)
        {
            dieEvent.Invoke();
        }
    }

    public void CallAttackSpeedEvent(float attackSpeed)
    {
        if(attackSpeedEvent != null)
        {
            attackSpeedEvent.Invoke(attackSpeed);
        }
    }
}
