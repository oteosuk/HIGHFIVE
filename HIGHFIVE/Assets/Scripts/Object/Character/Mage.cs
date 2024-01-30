using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{
    float curTime;
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
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            GameObject arrow = Main.ResourceManager.Instantiate("Character/MageWeapon", _playerStateMachine._player.transform.position);
            Vector2 dir = _playerStateMachine.targetObject.transform.position - arrow.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            arrow.transform.rotation = rotation;
            Rigidbody2D projectileRb = arrow.GetComponent<Rigidbody2D>();

            projectileRb.velocity = dir.normalized * 10.0f;
            curTime = _playerStateMachine._player.stat.AttackSpeed;
        }
    }
}
