using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LoadingAnimation : MonoBehaviour
{
    enum Sounds
    {
        Flash,
        HealPack,
        MageAttack01,
        MageQ,
        RogueAttack01,
        RogueQ,
        Trap,
        WarriorAttack01,
        WarriorQ
    }
    enum SkillSprite
    {
        Assessination,
        Berserk,
        FireBall,
        Flash,
        StunShot
    }

    public Image loadingBar;
    public TMP_Text loadingTxt;

    private int loadingCount = 0;
    private int totalCount = 0;
    private bool isSceneLoad = false;

    private int sounds = System.Enum.GetValues(typeof(Sounds)).Length;
    private int skillSprite = System.Enum.GetValues(typeof(SkillSprite)).Length;


    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private void Update()
    {
        if (!isSceneLoad)
        {
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].CustomProperties.TryGetValue("IsLoad", out object value))
                {
                    if ((bool)value == false) return;
                }
                else
                {
                    return;
                }
            }
            LoadingUI(1);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel((int)Define.Scene.GameScene);
            }
            isSceneLoad = true;
        }
    }
    private IEnumerator LoadFile()
    {
        // 전체 구하기
        totalCount = skillSprite + sounds ;

        for (int i = 0; i < skillSprite; i++)
        {
            yield return LoadPrefabAsync<Sprite>("Sprites/SkillIcon/" + (SkillSprite)i);
        }
        for (int i = 0; i < sounds; i++)
        {
            yield return LoadPrefabAsync<AudioClip>("Sounds/SFX/InGame/" + (Sounds)i);
        }
    }

    private IEnumerator LoadPrefabAsync<T>(string path) where T : Object
    {
        T loadObj = Main.ResourceManager.Load<T>(path);
        if (!Main.GameManager.InGameObj.ContainsKey(loadObj.name))
        {
            Main.GameManager.InGameObj.Add(loadObj.name, loadObj);
        }

        yield return null;

        float progress = (float)loadingCount / totalCount;
        LoadingUI(progress);

        loadingCount++;
    }

    private void LoadingUI(float progress)
    {
        loadingBar.fillAmount = progress;
        loadingTxt.text = "Loading " + Mathf.Clamp(Mathf.Floor(progress * 100), 0, 100) +"%";
    }

    IEnumerator LoadSceneProcess()
    {
        //게임 씬을 로드, 진행도를 확인
        yield return LoadFile();

        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            { "IsLoad", true }
        });
    }
}