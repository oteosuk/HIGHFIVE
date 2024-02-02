using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 오디오 소스
    public AudioSource bgmPlayer;
    public List<AudioSource> effectPlayer = new List<AudioSource>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            PlayEffect("SFX_Click", 1f);
        }
    }

    public void Init()
    {
        GameObject SoundManagerObject = GameObject.Find("SoundManager");

        if (SoundManagerObject == null)
        {
            SoundManagerObject = new GameObject("SoundManager");
            SoundManagerObject.AddComponent<SoundManager>();
        }
        
        bgmPlayer = Util.GetOrAddComponent<AudioSource>(SoundManagerObject);


        for (int i = 0; i < 10; i++)
        {
            AudioSource temp = SoundManagerObject.AddComponent<AudioSource>();
            effectPlayer.Add(temp);
        }

        DontDestroyOnLoad(SoundManagerObject);
    }

    public void PlayBGM(string bgmName, float volume)
    {
        bgmPlayer.clip =  Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.volume = volume;
            bgmPlayer.Play();
            
        }
    }

    public void PlayEffect(string efxName, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/SFX/{efxName}");
        Debug.Log(effectPlayer.Count);
        for (int i = 0; i < effectPlayer.Count; i++)
        {
            if (effectPlayer[i].isPlaying)
            {
                continue;
            }
            else
            {
                effectPlayer[i].volume = volume;
                effectPlayer[i].PlayOneShot(clip);
            }
        }
        //예외처리 필요, 10개보다 더 늘어날경우
    }

    // 음소거
    void Mute()
    {
        bgmPlayer.mute = !bgmPlayer.mute;
    }

    //사용법
    //SoundManager.instance.PlayEffect("효과음", 1f);
    //Main.SounManager.PlayEffect("파일이름", 볼륨); => 아직 연결안됌
}
