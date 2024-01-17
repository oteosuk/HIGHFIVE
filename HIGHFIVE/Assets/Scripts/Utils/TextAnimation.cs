using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public TMP_Text loadingText;
    public float animationSpeed = 0.15f;
    private int dotCount = 1;

    void Start()
    {
        // 시작할 때 코루틴을 시작하여 텍스트 애니메이션을 실행
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        while (true)
        {
            // 텍스트를 설정
            loadingText.text = "접속중입니다" + new string('.', dotCount);

            // dotCount를 1, 2, 3, 1, 2, 3, ... 순서로 증가
            dotCount = (dotCount % 3) + 1;

            // 지정된 시간 동안 기다림
            yield return new WaitForSeconds(animationSpeed);
        }
    }
}
