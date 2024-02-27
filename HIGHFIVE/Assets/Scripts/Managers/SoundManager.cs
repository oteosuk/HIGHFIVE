using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager
{
    // 오디오 소스
    private AudioSource bgmPlayer;
    private List<AudioSource> sfxPlayer = new List<AudioSource>();
    //private AudioMixer audioMixer;
    private float bgmVolume;
    private float effectVolume;
    public Dictionary<string, AudioClip> EffectDict { get; private set; } = new Dictionary<string, AudioClip>();

    public void SoundInit()
    {
        GameObject musicObject = GameObject.Find("SoundManager");

        if (musicObject == null)
        {
            musicObject = new GameObject("SoundManager");
            musicObject.GetOrAddComponent<AudioSource>();
        }

        Object.DontDestroyOnLoad(musicObject);
        bgmPlayer = Util.GetOrAddComponent<AudioSource>(musicObject);

        for (int i = 0; i < 10; i++)
        {
            AudioSource temp = musicObject.AddComponent<AudioSource>();
            sfxPlayer.Add(temp);
        }
    }

    public void PlayBGM(string bgmName)
    {
        bgmPlayer.clip =  Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.volume = bgmVolume;
            bgmPlayer.Play();
            bgmPlayer.loop = true;
        }
    }

    public void PlaySFX(string sfxName, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/SFX/{sfxName}");
        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (sfxPlayer[i].isPlaying)
            {
                continue;
            }
            else
            {
                sfxPlayer[i].volume = volume;
                sfxPlayer[i].PlayOneShot(clip);
            }
        }
        //예외처리 필요, 10개보다 더 늘어날경우
    }
    public void PlayEffect(AudioSource source)
    {
        if (source.clip == null || source == null) return;

        Debug.Log(effectVolume);
        source.volume = effectVolume;
        source.PlayOneShot(source.clip);
    }

    public void SetBGMVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }

    public void SetEffectVolume(float volume)
    {
        effectVolume = volume;
    }
}