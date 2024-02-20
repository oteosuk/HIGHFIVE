using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueNormalAttack : MonoBehaviour
{
    public  void OnNormalAttack()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == PhotonNetwork.LocalPlayer)
            {
                Character myCharacter = Main.GameManager.SpawnedCharacter;
                if (myCharacter.targetObject != null && myCharacter.targetObject.layer != (int)Define.Layer.Default)
                {
                    myCharacter.targetObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, transform.parent.gameObject);
                }
            }
        }
        
    }
}
