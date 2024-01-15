using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private float time;
    [SerializeField] private float curTime;

    // int minute;
    int second;

    private void Awake()
    {
        time = 20;
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
                Debug.Log("시간 종료");
                // 시간 내에 못 고르면 자동으로 선택되게 하는 로직 추가
                curTime = 0;
                yield break;
            }
        }
    }
}
