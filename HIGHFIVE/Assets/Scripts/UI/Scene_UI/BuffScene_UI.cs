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
        _buffController.addBuffEvent += AddBuff;
    }

    private void AddBuff(BaseBuff buff)
    {
        GameObject buffObj = Main.ResourceManager.Instantiate("UI_Prefabs/Buff", transform);

        buff.buffData.coolTimeicon = buffObj.transform.Find("BuffCool").GetComponent<Image>();
        buffObj.GetComponent<Image>().sprite = buff.buffData.buffSprite;
    }
}
