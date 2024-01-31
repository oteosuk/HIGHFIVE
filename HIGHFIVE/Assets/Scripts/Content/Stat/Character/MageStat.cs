using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStat : CharacterStat
{

    protected override void Init()
    {
        base.Init();

        if (Main.DataManager.CharacterDict.TryGetValue("마법사", out CharacterDBEntity mage))
        {
            CurHp = mage.curHp;
            MaxHp = mage.maxHp;
            Attack = mage.atk;
            AttackRange = mage.atkRange;
            AttackSpeed = mage.atkSpeed;
            MoveSpeed = mage.movSpeed;
            Defence = mage.def;
            SightRange = mage.sight;
        }
    }
}
