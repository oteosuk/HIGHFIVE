using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    public List<int> levelExpList = new List<int>();

    private int _curHp;
    private int _maxHp;
    private int _attack;
    private int _defence;
    private float _attackRange;
    private float _attackSpeed;
    private float _moveSpeed;
    private float _sightRange;
    private int _exp;
    private int _maxExp;
    [HideInInspector]
    public StatController _statController;
    public int CurHp
    {
        get { return _curHp; }
        set { _statController.CallChangeHpEvent(value, MaxHp); _curHp = value; }
    }
    public int MaxHp 
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    public int Attack
    {
        get { return _attack; }
        set { _statController.CallChangeAttackEvent(value); _attack = value; }
    }
    public int Defence
    {
        get { return _defence; }
        set { _statController.CallChangeDefenceEvent(value); _defence = value; }
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
    public int MaxExp
    {
        get { return _maxExp; }
        set { _maxExp = value; }
    }
    public int Exp 
    {
        get { return _exp; }
        set { _statController.CallChangeExpEvent(value, MaxExp); _exp = value; }
    }
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _statController = Util.GetOrAddComponent<StatController>(gameObject);
        InitializeExp();
    }

    public virtual void TakeDamage(int damage, GameObject shooter)
    {
        Stat myStat = GetComponent<Stat>();
        int realDamage = Mathf.Max(0, damage - myStat.Defence);
        if (myStat.CurHp - realDamage <= 0)
        {
            if (gameObject.tag != "Player")
            {
                shooter.GetComponent<CharacterStat>().AddExp(myStat.Exp, shooter);
            }
            myStat.CurHp = 0;
        }
        else
        {
            myStat.CurHp -= realDamage;
        }
        myStat.gameObject.GetComponent<PhotonView>().RPC("SetHpRPC", RpcTarget.All, myStat.CurHp);
    }

    public virtual void TakeDamage(int damage)
    {
        Stat myStat = GetComponent<Stat>();
        int realDamage = Mathf.Max(0, damage - myStat.Defence);
        if (myStat.CurHp - realDamage <= 0)
        {
            myStat.CurHp = 0;
        }
        else
        {
            myStat.CurHp -= realDamage;
        }
        myStat.gameObject.GetComponent<PhotonView>().RPC("SetHpRPC", RpcTarget.All, myStat.CurHp);
    }

    private void InitializeExp()
    {
        int baseExperience = 10;

        // 0번째 인덱스에 0값 추가
        levelExpList.Add(0);

        for (int level = 1; level <= 10; level++)
        {
            levelExpList.Add(baseExperience);
            baseExperience *= 2; // 각 레벨마다 경험치를 2배로 증가
        }
    }
}
