using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager2 : MonoBehaviour
{
    public AudioClip[] mainBgm;
    public AudioClip deathBgm;

    public AudioSource audioSource;

    private AudioSource bgmPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.volume = 0.4f;
        //재생할 bgm 번호 선택하고 재생하기

        //audioSource.volume = 1.0f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절

        //audioSource.mute = false; //오디오 음소거

        //audioSource.Stop(); //오디오 멈추기


        //audioSource.priority = 0;
        //씬안에 모든 오디오소스중 현재 오디오 소스의 우선순위를 정한다.
        // 0 : 최우선, 256 : 최하, 128 : 기본값
    }

    //void FixedUpdate()
    //{
    //    BGM();
    //}

    public void PlayBGM(string bgmName, float volume)
    {
        bgmPlayer.clip = Resources.Load<AudioClip>($"Sounds/BGM/{bgmName}");
        if (bgmPlayer.clip != null)
        {
            bgmPlayer.volume = volume;
            bgmPlayer.Play();
        }
    }


    void PlayDeathBgm()
    {
        //기존 bgm 중지
        audioSource.Stop();

        audioSource.volume = 1f;

        //사망 Bgm 재생, PlayOneShot은 무조건 한 번만 재생됨 루프 안됨
        audioSource.PlayOneShot(deathBgm);
    }


    public void BGM()
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        { 
            PlayBGM("Town_Castle_01", 0.02f); 
        }

        //else if (SceneManager.GetActiveScene().name == "RoomScene")
        //{ PlayBGM("Town_Castle_01", 0.02f); } 

        else if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            audioSource.Stop();
            PlayBGM("Battle_Normal_EW02", 0.02f); 
        }

        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            audioSource.Stop(); 
            PlayBGM("Battle_Boss_07", 0.02f); 
        }
    }

}
