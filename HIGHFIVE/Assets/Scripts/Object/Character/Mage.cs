using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log(GetComponent<Stat>().Exp);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnNormalAttack()
    {
        base.OnNormalAttack();
        if (_playerStateMachine._player.targetObject != null)
        {
            isFistTime = false;
            GameObject sphere = Main.ResourceManager.Instantiate("Character/MageWeapon", _playerStateMachine._player.transform.position, syncRequired:true);
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
