using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueStat : CharacterStat
{
    protected override void Init()
    {
        base.Init();

        if (Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue))
        {
            CurHp = rogue.curHp;
            MaxHp = rogue.maxHp;
            Attack = rogue.atk;
            AttackRange = rogue.atkRange;
            AttackSpeed = rogue.atkSpeed;
            MoveSpeed = rogue.movSpeed;
            Defence = rogue.def;
            SightRange = rogue.sight;
        }

    }
}
