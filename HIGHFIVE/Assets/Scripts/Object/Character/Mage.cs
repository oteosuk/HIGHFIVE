using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : Character
{
    [SerializeField] Transform _tip;
    protected override void Awake()
    {
        base.Awake();
        stat = GetComponent<Stat>();
        CharacterSkill = GetComponent<MageSkills>();
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
        base.OnNormalAttack();
        if (_playerStateMachine._player.targetObject != null && _playerStateMachine._player.targetObject.layer != (int)Define.Layer.Default)
        {
            Main.ResourceManager.Instantiate("Character/MageWeapon", _tip.position, syncRequired: true);
        }
    }
}
