using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStat : MonsterStat
{
    protected override void Init()
    {
        base.Init();

        if(Main.DataManager.MonsterDict.TryGetValue("나무", out MonsterDBEntity tree))
        {
            CurHp = tree.curHp;
            MaxHp = tree.maxHp;
            Attack = tree.atk;
            AttackRange = tree.atkRange;
            AttackSpeed = tree.atkSpeed;
            MoveSpeed = tree.movSpeed;
            Defence = tree.def;
            SightRange = tree.sight;
            Exp = tree.exp;
        }
    }
}
