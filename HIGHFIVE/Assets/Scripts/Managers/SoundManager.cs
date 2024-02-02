using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 오디오 소스
    private AudioSource bgmPlayer;
    private List<AudioSource> effectPlayer = new List<AudioSource>();

    private List<SoundData> soundDatas;

    // 오디오 클립
    private AudioClip[] musics;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            effectPlayer.Add(temp);
        }
        PlayBGM("KBF_3m_Town_Castle_01", 1f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 클릭된 위치의 좌표를 가져옵니다.
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 클릭 이벤트 처리 메서드 호출
            HandleClick(clickPosition);
        }
    }

    void HandleClick(Vector2 clickPosition)
    {
        PlayEffect("SFX_Click", 1f);
        Debug.Log("마우스가 클릭되었습니다! 위치: " + clickPosition);
    }


    public void PlayBGM(string name, float volume)
    {
        var selectd = Array.Find(musics, m => m.name == name);
        bgmPlayer.clip = selectd;
        bgmPlayer.volume = volume;
        bgmPlayer.Play();
    }

    public void ChangeBGM(string name, AudioClip newAudioClip)
    {
        SoundData test = soundDatas.Find(sd => sd.soundName == name);

        if(test != null)
        {
            test.audioClip = newAudioClip;
        }
    }

    public void PlayEffect(string name, float volume)
    {
        var selectd = Array.Find(musics, m => m.name == name);
        
        for (int i = 0; i < effectPlayer.Count; i++)
        {
            if (effectPlayer[i].isPlaying)
                continue;
            else
            {
                effectPlayer[i].volume = volume;
                effectPlayer[i].PlayOneShot(selectd);
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
