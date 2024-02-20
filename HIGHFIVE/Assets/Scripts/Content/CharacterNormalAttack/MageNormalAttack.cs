using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageNormalAttack : MonoBehaviour
{
    public void OnNormalAttack()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == PhotonNetwork.LocalPlayer)
            {
                Character myCharacter = Main.GameManager.SpawnedCharacter;
                if (myCharacter.targetObject != null && myCharacter.targetObject.layer != (int)Define.Layer.Default)
                {
                    Main.ResourceManager.Instantiate("Character/MageWeapon", transform.position, syncRequired: true);
                }
            }
        }

        
    }
}
