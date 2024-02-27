using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectSlider;

    void Start()
    {
        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (effectSlider != null)
        {
            effectSlider.onValueChanged.AddListener(SetEffectVolume);
        }
    }
    public void SetBGMVolume(float volume)
    {
        Main.SoundManager.SetBGMVolume(volume);
    }

    public void SetEffectVolume(float volume)
    {
        Main.SoundManager.SetEffectVolume(volume);
    }
}
