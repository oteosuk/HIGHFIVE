using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public List<BaseBuff> onBuffList = new List<BaseBuff>();
    public event Action<BaseBuff> addBuffEvent;
    public event Action<BaseBuff> cancelBuffEvent;

    public void AddBuffEvent(BaseBuff buff)
    {
        if (addBuffEvent != null)
        {
            addBuffEvent.Invoke(buff);
        }
    }

    public void CancelBuffEvent(BaseBuff buff)
    {
        if (cancelBuffEvent != null)
        {
            cancelBuffEvent.Invoke(buff);
        }
    }

    public BaseBuff FindBuff<T>() where T : BaseBuff
    {
        foreach (BaseBuff buff in onBuffList)
        {
            if (typeof(T) == buff.GetType()) return buff;
        }
        return null;
    }

    public void AddBuff(BaseBuff buff, GameObject shooter = null)
    {
        foreach (BaseBuff hasBuff in onBuffList)
        {
            if (buff.GetType() == hasBuff.GetType())
            {
                Refill(hasBuff);
                return;
            }
        }

        onBuffList.Add(buff);
        buff.Init();
        if (gameObject.GetComponent<Character>())
        {
            AddBuffEvent(buff);
        }
        buff.Activation();
        StartCoroutine(buff.ApplyEffect(gameObject, shooter));
        StartCoroutine(BuffTimer(buff));
    }
     
    IEnumerator BuffTimer(BaseBuff buff)
    {
        while (buff.buffData.curTime < buff.buffData.duration)
        {
            buff.buffData.curTime += 0.1f;
            if (gameObject.GetComponent<Character>())
            {
                buff.buffData.coolTimeicon.fillAmount = buff.buffData.curTime / buff.buffData.duration;
            }
            yield return new WaitForSeconds(0.1f);
        }
        buff.buffData.curTime = 0;
        RemoveBuff(buff);
    }

    private void RemoveBuff(BaseBuff buff)
    {
        if (onBuffList.Contains(buff))
        {
            if (gameObject.GetComponent<Character>())
            {
                CancelBuffEvent(buff);
            }
            onBuffList.Remove(buff);
            buff.Deactivation();
        }
    }

    public void CancelBuff(BaseBuff buff)
    {
        if (onBuffList.Contains(buff))
        {
            buff.buffData.curTime = buff.buffData.duration;
        }
    }

    public void CancelUnSustainBuff()
    {
        foreach (BaseBuff buff in onBuffList)
        {
            if (!buff.buffData.isSustainBuff)
            {
                CancelBuff(buff);
            }
        }
    }


    public void Refill(BaseBuff buff)
    {
        buff.buffData.curTime = 0;
        buff.buffData.effectTime = 0;
    }
}
