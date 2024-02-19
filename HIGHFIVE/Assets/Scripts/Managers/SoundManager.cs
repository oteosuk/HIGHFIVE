using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // 오디오 소스
    private AudioSource bgmPlayer;
    private List<AudioSource> sfxPlayer = new List<AudioSource>();
    private AudioMixer audioMixer;

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

        PlayBGM("Town_Castle_01", 0.02f);
        // BGM();
    }

    public void SoundUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Main.SoundManager.PlaySFX("SFX_Click", 0.5f);
        }
    }

    public void BGM()
    {
        if (SceneManager.GetActiveScene().name == "IntroScene")
        { PlayBGM("Town_Castle_01", 0.02f); }

        else if (SceneManager.GetActiveScene().name == "SelectScene")
        { PlayBGM("Battle_Normal_EW02", 0.02f); }

        else if (SceneManager.GetActiveScene().name == "GameScene")
        { PlayBGM("Battle_Boss_07", 0.02f); }
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
        //Debug.Log(effectPlayer.Count);
        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (sfxPlayer[i].isPlaying)
            {
                //Debug.Log(i);
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

    // 음소거
    void Mute()
    {
        bgmPlayer.mute = !bgmPlayer.mute;
    }

    //사용법
    //SoundManager.instance.PlayEffect("효과음", 1f);
    //Main.SoundManager.PlayEffect("파일이름", 볼륨); => 아직 미연결
}