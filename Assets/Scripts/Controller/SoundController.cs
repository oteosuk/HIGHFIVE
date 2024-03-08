using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider effectSlider;

    void Awake()
    {
        bgmSlider.value = Main.SoundManager.bgmVolume;
        effectSlider.value = Main.SoundManager.effectVolume;
        SetBGMVolume(Main.SoundManager.bgmVolume);
        SetEffectVolume(Main.SoundManager.effectVolume);
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
