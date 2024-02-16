using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;

public class CharSelectTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float time;
    [SerializeField] private float curTime;

    // int minute;
    int second;

    private void Awake()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            // minute = (int)curTime / 60;
            second = (int)curTime % 60;
            text.text = second.ToString("0");
            yield return null;

            if (curTime <= 0)
            {
                PhotonNetwork.LoadLevel((int)Define.Scene.LoadingScene);
                curTime = 0;
                yield break;
            }
        }
    }
}
