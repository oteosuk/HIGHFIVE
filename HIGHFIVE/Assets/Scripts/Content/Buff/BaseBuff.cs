using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BuffData
{
    public Type type;
    public float duration;
    public float curTime;
    public Image coolTimeicon;
    public Sprite buffSprite;
}

public class BaseBuff : MonoBehaviour
{
    public BuffData buffData;
    protected BuffController _buffController;
    protected Stat _stat;

    protected virtual void Awake()
    {
        _buffController = Main.GameManager.SpawnedCharacter.GetComponent<BuffController>();
        _stat = Main.GameManager.SpawnedCharacter.GetComponent<Stat>();
    }
    protected virtual void Start()
    {
        _buffController.onBuffList.Add(this.GetType());
    }

    protected virtual IEnumerator ActiveBuff()
    {
        while(buffData.curTime < buffData.duration)
        {
            buffData.curTime += 0.1f;
            buffData.coolTimeicon.fillAmount = buffData.curTime / buffData.duration;
            yield return new WaitForSeconds(0.1f);
        }
        buffData.coolTimeicon.fillAmount = 1;
        buffData.curTime = 0;
        LoseBuff();
    }

    protected virtual void LoseBuff()
    {
        _buffController.onBuffList.Remove(this.GetType());
        Main.ResourceManager.Destroy(gameObject);
    }

    public virtual void Refill() { }
}
