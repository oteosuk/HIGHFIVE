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
        MaxExp = levelExpList[Level];
    }

    public void AddExp(int exp, GameObject go)
    {
        CharacterStat myStat = go.GetComponent<CharacterStat>();
        if (myStat != null ) 
        {
            
            if (myStat.Exp == myStat.MaxExp) return;
            int maxExp = myStat.MaxExp;
            
            if (myStat.Exp + exp >= maxExp)
            {
                LevelUp(exp);
            }
            else
            {
                myStat.Exp += exp;
                _statController.CallChangeExpEvent(myStat.Exp, myStat.MaxExp);
            }
        }
    }
    private void LevelUp(int exp)
    {
        CharacterStat myStat = GetComponent<CharacterStat>();
        if (myStat != null ) 
        { 
            if (myStat.Level == 10) return;
            myStat.Level += 1;
            myStat.Exp = myStat.Exp + exp - myStat.MaxExp;
            myStat.MaxExp += 10;
            //myStat.MaxExp = myStat.levelExpList[myStat.Level];
            myStat.Attack += 2;
            myStat.Defence += 1;
            
            if (myStat.Exp > myStat.MaxExp)
            {
                LevelUp(exp);
            }
        }
    }

}
