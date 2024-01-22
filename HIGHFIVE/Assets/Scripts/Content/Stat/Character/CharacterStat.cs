using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Stat
{
    public int Exp { get; set; }
    public int Level { get; set; }
    protected override void Init()
    {
        base.Init();
    }
}
