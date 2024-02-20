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
        }
    }
}
