using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Stat
{
    public List<int> levelExpList = new List<int>();
    public int Level { get; set; }
    public int MaxExp { get; set; }
    protected override void Init()
    {
        base.Init();
        InitializeExp();
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
    private void InitializeExp()
    {
        int baseExperience = 20;

        // 0번째 인덱스에 0값 추가
        levelExpList.Add(0);

        for (int level = 1; level <= 10; level++)
        {
            levelExpList.Add(baseExperience);
            baseExperience *= 2; // 각 레벨마다 경험치를 2배로 증가
        }
    }

    
}
