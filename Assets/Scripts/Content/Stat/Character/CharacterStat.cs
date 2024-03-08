using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : Stat
{
    private int _level;

    public int Level
    {
        get { return _level; }
        set { _statController.CallChangeLevelEvent(value); _level = value; }
    }
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
            int maxExp = myStat.MaxExp;
            myStat.Exp += exp;
            if (myStat.Exp >= maxExp)
            {
                int restExp = myStat.Exp - maxExp;
                LevelUp(restExp);
            }
        }
    }
    private void LevelUp(int exp)
    {
        CharacterStat myStat = GetComponent<CharacterStat>();
        if (myStat != null ) 
        {
            if (myStat.Level == 15) return;
            myStat.Level += 1;
            myStat.MaxExp += 10;
            myStat.Exp = exp;
            myStat.Attack += 3;
            myStat.Defence += 1;
            myStat.AttackSpeed += 0.1f;
            myStat.MaxHp += 40;
            if (myStat.CurHp > 0)
            {
                if (myStat.MaxHp < myStat.CurHp + (int)((myStat.MaxHp / 1) * 0.2))
                {
                    myStat.CurHp = myStat.MaxHp;
                }
                else { myStat.CurHp += (int)((myStat.MaxHp / 1) * 0.2); }
            }
            
            PhotonView pv = myStat.gameObject.GetComponent<PhotonView>();
            pv.RPC("SyncLevel", RpcTarget.All, myStat.Level, pv.ViewID);
            pv.RPC("SetHpRPC", RpcTarget.Others, myStat.CurHp, myStat.MaxHp);
            

            if (myStat.Exp > myStat.MaxExp)
            {
                LevelUp(myStat.Exp - myStat.MaxExp);
            }
        }
    }

}
