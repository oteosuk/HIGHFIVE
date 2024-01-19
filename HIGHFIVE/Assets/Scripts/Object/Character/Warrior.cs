using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    private WarriorStat _warriorStat;
    private void Start()
    {
        _warriorStat = Util.GetOrAddComponent<WarriorStat>(gameObject);
    }

}
