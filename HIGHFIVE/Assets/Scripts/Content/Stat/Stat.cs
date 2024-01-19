using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public int CurHp { get; set; }
    public int MaxHp { get; set; }
    public int Attack { get;  set; }
    public int Defence { get; set; }
    public float AttackRange { get; set; }
    public float AttackSpeed { get; set; }
    public float MoveSpeed { get; set; }
    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {

    }
}
