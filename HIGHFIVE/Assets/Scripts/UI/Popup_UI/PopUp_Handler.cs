using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUp_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // DOTween 기본설정
        DOTween.Init();

        transform.localScale = Vector3.one * 0.1f;
    }

    public void Show()
    {
        gameObject.SetActive(true);

        // 트윈을 순차적으로 실행할 수 있는 시퀀스를 만든다. 
        var set = DOTween.Sequence();

        set.Append(transform.DOScale(1.1f, 0.2f));
        set.Append(transform.DOScale(1f, 0.1f));

        set.Play();
    }

    public void Hide()
    {
        // 트윈을 순차적으로 실행할 수 있는 시퀀스를 만든다. 
        var set = DOTween.Sequence();

        // Append는 시퀀스를 추가하는 데 사용된다.
        set.Append(transform.DOScale(1.1f, 0.2f));
        set.Append(transform.DOScale(0.2f, 0.2f));

        // set이 시퀀스를 모두 완료하면 => { } 안에 명령어들을 실행한다.
        set.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
