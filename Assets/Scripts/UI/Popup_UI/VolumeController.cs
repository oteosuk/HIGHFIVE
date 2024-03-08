using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl(float volume)
    {
        masterMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

}
