using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageNormalAttack : MonoBehaviour
{
    public void OnNormalAttack()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        if (myCharacter.targetObject != null && myCharacter.targetObject.layer != (int)Define.Layer.Default)
        {
            GameObject sphere = Main.ResourceManager.Instantiate("Character/MageWeapon", transform.position, syncRequired: true);
            sphere.transform.position = transform.position;
            Vector2 dir = myCharacter.targetObject.transform.position - sphere.transform.position;
            PhotonView targetPhotonView = Util.GetOrAddComponent<PhotonView>(myCharacter.targetObject);

            sphere.GetComponent<ShooterInfoController>().CallShooterInfoEvent(transform.parent.gameObject);
            sphere.GetComponent<PhotonView>().RPC("SetTarget", RpcTarget.All, targetPhotonView.ViewID);
            sphere.GetComponent<PhotonView>().RPC("ToTarget", RpcTarget.All, 5.0f, dir.x, dir.y);
        }
    }
}
