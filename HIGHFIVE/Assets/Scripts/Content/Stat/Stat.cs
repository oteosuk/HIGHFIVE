using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private int _curHp;
    private int _maxHp;
    private int _attack;
    private int _defence;
    private float _attackRange;
    private float _attackSpeed;
    private float _moveSpeed;
    private float _sightRange;
    private int _exp;
    private StatController _statController;
    public int CurHp
    {
        get { return _curHp; }
        set { _statController.CallChangeHpEvent(CurHp, MaxHp); _curHp = value; }
    }
    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    public int Attack
    {
        get { return _attack; }
        set { _attack = value; }
    }
    public int Defence
    {
        get { return _defence; }
        set { _defence = value; }
    }
    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }
    public float SightRange
    {
        get { return _sightRange; }
        set { _sightRange = value; }
    }
    public int Exp 
    {
        get { return _exp; }
        set { _exp = value; }
    }
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _statController = Util.GetOrAddComponent<StatController>(gameObject);
    }
    public virtual void TakeDamage(int damage)
    {
        Stat myStat = GetComponent<Stat>();
        int realDamage = Mathf.Max(0, damage - myStat.Defence);
        if (myStat.CurHp - realDamage <= 0)
        {
            myStat.CurHp = 0;
            return;
        }
        myStat.CurHp -= realDamage;
    }

}
