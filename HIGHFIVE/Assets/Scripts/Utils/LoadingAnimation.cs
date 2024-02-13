using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LoadingAnimation : MonoBehaviour
{
    public CanvasGroup _canvasGroup;
    public Image _image;
    private bool fadeInStarted = false;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        if (!fadeInStarted)
        {
            CanvasFadeIn();
            fadeInStarted = true;
        }
    }

    private void ImageAnimateion()
    {
        _image.DOFillAmount(1f, 3f)
            .SetDelay(1f)
            .OnComplete(CanvasFadeOut);
    }

    private void CanvasFadeIn()
    {
        if (_canvasGroup)
        {
            _canvasGroup.DOFade(1f, 1f)
                .OnComplete(ImageAnimateion);
        }
        else
        {
            Debug.LogError("Canvas가 없어2");
        }
    }

    private void CanvasFadeOut()
    {
        _canvasGroup.DOFade(0f, 1f)
            .OnComplete(LoadNextScene);
    }

    private void LoadNextScene()
    {
        Main.SceneManagerEx.LoadScene(Define.Scene.GameScene);
    }
}
