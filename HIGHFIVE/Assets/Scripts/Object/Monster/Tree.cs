using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Tree : Monster
{
    private double _curDelay;
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnNormalAttack()
    {
        if (targetObject != null)
        {
            targetObject.GetComponent<Stat>()?.TakeDamage(stat.Attack);
        }
        _curDelay = 1.0 / stat.AttackSpeed;
    }
}
