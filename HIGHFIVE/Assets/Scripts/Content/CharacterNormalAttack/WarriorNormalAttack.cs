using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorNormalAttack : MonoBehaviour
{
    private BuffController _buffController;
    private void Start()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _buffController = myCharacter.GetComponent<BuffController>();
    }
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
                    BaseBuff buff = _buffController.FindBuff<StabbingBuff>();
                    if (buff != null)
                    {
                        myCharacter.targetObject.GetComponent<Stat>()?.TakeDamage(buff.buffData.damage, transform.parent.gameObject);
                    }
                    else
                    {
                        myCharacter.targetObject.GetComponent<Stat>()?.TakeDamage(Main.GameManager.SpawnedCharacter.stat.Attack, transform.parent.gameObject);
                    }
                }
            }
        }
        
    }
}
