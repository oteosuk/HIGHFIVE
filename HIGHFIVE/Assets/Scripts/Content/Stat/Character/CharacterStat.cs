using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Stat
{

    public int Level { get; set; }
    protected override void Init()
    {
        base.Init();
        Exp = 0;
        Level = 1;
    }

    public void AddExp(int exp)
    {
        CharacterStat myStat = GetComponent<CharacterStat>();
        if (myStat != null ) 
        { 
            if (myStat.Exp == myStat.MaxExp) return;
            int maxExp = myStat.MaxExp;
            if (myStat.Exp + exp >= maxExp)
            {
                LevelUp();
                myStat.Exp = myStat.Exp + exp - maxExp;
            }
            else
            {
                myStat.Exp += exp;
            }
        }
    }
    private void LevelUp()
    {
        CharacterStat myStat = GetComponent<CharacterStat>();
        if (myStat != null ) 
        { 
            if (myStat.Level == 10) return;
            myStat.Level += 1;
            myStat.Exp = 0;
            myStat.MaxExp = myStat.levelExpList[myStat.Level];
            myStat.Attack += 2;
            myStat.Defence += 1;
            myStat.MaxExp += 30;
        }
    }

}
