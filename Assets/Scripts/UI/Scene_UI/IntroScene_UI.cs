using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroScene_UI : MonoBehaviour
{
    public LoopType loopType;
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.DOFade(0.0f, 1).SetLoops(-1, loopType);
    }
}
