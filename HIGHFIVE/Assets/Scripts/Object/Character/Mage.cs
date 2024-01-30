using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    protected override void Awake()
    {
        base.Awake();
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

    [PunRPC]
    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public override void OnNormalAttack()
    {
        base.OnNormalAttack();
        if (_playerStateMachine._player.targetObject != null)
        {
            GameObject arrow = Main.ResourceManager.Instantiate("Character/MageWeapon", _playerStateMachine._player.transform.position);
            arrow.transform.position = transform.position;
            arrow.GetComponent<MageWeapon>().SetTarget(_playerStateMachine._player.targetObject);
        }
    }
}
