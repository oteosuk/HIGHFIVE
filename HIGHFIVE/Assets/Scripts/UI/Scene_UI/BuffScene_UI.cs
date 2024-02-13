using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffScene_UI : MonoBehaviour
{
    private BuffController _buffController;
    private void Start()
    {
        _buffController = Main.GameManager.SpawnedCharacter.GetComponent<BuffController>();
        _buffController.buffEvent += AddBuff;
    }

    private void AddBuff(Type buffType)
    {
        foreach (Type type in _buffController.onBuffList)
        {
            if (type == buffType)
            {
                transform.Find(buffType.ToString()).GetComponent<BaseBuff>().Refill();
                return;
            }
        }
        GameObject buff = Main.ResourceManager.Instantiate("UI_Prefabs/Buff", transform, buffType.ToString());
        buff.AddComponent(buffType);
    }
}
