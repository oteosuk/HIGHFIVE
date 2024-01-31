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
           
            GameObject arrow = Main.ResourceManager.Instantiate("Character/MageWeapon", _playerStateMachine._player.transform.position, syncRequired:true);
            arrow.transform.position = transform.position;
            Vector2 dir = _playerStateMachine._player.targetObject.transform.position - arrow.transform.position;
            PhotonView targetPhotonView = Util.GetOrAddComponent<PhotonView>(_playerStateMachine._player.targetObject);
            Debug.Log(targetPhotonView.ViewID);
            if (targetPhotonView.ViewID == 0 )
            {
                PhotonNetwork.AllocateViewID(targetPhotonView);
            }
            
            arrow.GetComponent<PhotonView>().RPC("SetTarget", RpcTarget.All, targetPhotonView.ViewID);
            arrow.GetComponent<PhotonView>().RPC("ToTarget", RpcTarget.All, 5.0f, dir.x, dir.y);
        }
    }
}
