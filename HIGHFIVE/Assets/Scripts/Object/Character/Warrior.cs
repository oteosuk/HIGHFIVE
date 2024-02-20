using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<WarriorSkill>();
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
    //public override void OnNormalAttack()
    //{
    //    base.OnNormalAttack();
    //    if (_playerStateMachine._player.targetObject != null && _playerStateMachine._player.targetObject.layer != (int)Define.Layer.Default)
    //    {
    //        BaseBuff buff = BuffController.FindBuff<StabbingBuff>();
    //        if (buff != null)
    //        {
    //            _playerStateMachine._player.targetObject.GetComponent<Stat>()?.TakeDamage(buff.buffData.damage, gameObject);
    //        }
    //        else
    //        {
    //            _playerStateMachine._player.targetObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, gameObject);
    //        }
    //    }
    //}
}
