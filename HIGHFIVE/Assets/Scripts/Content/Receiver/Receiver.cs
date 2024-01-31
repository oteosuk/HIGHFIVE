using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour, IDamagable
{
    public void TakeDamage(int damage) 
    {
        Stat characterStat = GetComponent<Monster>().stat;
        if (characterStat != null )
        {
            int realDamage = Mathf.Max(0, damage - characterStat.Defence);
            characterStat.CurHp -= realDamage;
            Debug.Log(characterStat.CurHp);
        }
    }


}
