using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStat : CharacterStat
{
    protected override void Init()
    {
        base.Init();

        if (Main.DataManager.CharacterDict.TryGetValue("전사", out CharacterDBEntity warrior))
        {
            CurHp = warrior.curHp;
            MaxHp = warrior.maxHp;
            Attack = warrior.atk;
            AttackRange = warrior.atkRange;
            AttackSpeed = warrior.atkSpeed;
            MoveSpeed = warrior.movSpeed;
            Defence = warrior.def;
            SightRange = warrior.sight;
        }

    }
}
