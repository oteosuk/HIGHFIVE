using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : Receiver, IDamagable
{
    public void TakeDamage(int damage) 
    {
        Stat characterStat = GetComponent<Stat>();
        if (characterStat != null )
        {
            int realDamage = Mathf.Max(0, damage - characterStat.Defence);
            characterStat.CurHp -= realDamage;
        }
    }


}
