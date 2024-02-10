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
            GameObject sphere = Main.ResourceManager.Instantiate("Character/MageWeapon", _tip.position, syncRequired:true);
            sphere.transform.position = transform.position;
            Vector2 dir = _playerStateMachine._player.targetObject.transform.position - sphere.transform.position;
            PhotonView targetPhotonView = Util.GetOrAddComponent<PhotonView>(_playerStateMachine._player.targetObject);

            if (targetPhotonView.ViewID == 0 )
            {
                PhotonNetwork.AllocateViewID(targetPhotonView);
            }

            sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(gameObject);
            sphere.GetComponent<PhotonView>().RPC("SetTarget", RpcTarget.All, targetPhotonView.ViewID);
            sphere.GetComponent<PhotonView>().RPC("ToTarget", RpcTarget.All, 5.0f, dir.x, dir.y);
        }
    }
}
