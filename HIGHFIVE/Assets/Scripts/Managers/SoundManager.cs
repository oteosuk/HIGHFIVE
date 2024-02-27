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
    private AudioMixer audioMixer;
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

        PlayBGM("Battle_Normal_EW01_B", 0.1f);
    }

    public void PlayBGM(string bgmName, float volume)
    {
        bgmPlayer.clip =  Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.volume = volume;
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
        
        source.PlayOneShot(source.clip);
    }

    // 음소거
    void Mute()
    {
        bgmPlayer.mute = !bgmPlayer.mute;
    }

    //사용법
    //SoundManager.instance.PlayEffect("효과음", 1f);
    //Main.SoundManager.PlayEffect("파일이름", 볼륨); => 아직 미연결
}