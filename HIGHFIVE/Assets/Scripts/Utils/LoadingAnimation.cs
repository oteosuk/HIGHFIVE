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
    enum Character
    {
        Warrior,
        Rogue,
        Mage
    }

    enum NormalMonster
    {
        Tree,
        GreenOrc1,
        GreenOrc2,
    }

    enum UI
    {
        GameSceneUI
    }

    public Image loadingBar;
    public TMP_Text loadingTxt;

    private int loadingCount = 0;
    private int totalCount = 0;
    private bool isAllReady = false;

    private int character = System.Enum.GetValues(typeof(Character)).Length;
    private int normalMonster = System.Enum.GetValues(typeof(NormalMonster)).Length;
    private int ui = System.Enum.GetValues(typeof(UI)).Length;

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    private void Update()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].CustomProperties.TryGetValue("IsLoad", out object value))
            {
                Debug.Log((bool)value);
                if ((bool)value == false) return;
            }
        }

        isAllReady = true;
        if (isAllReady) { PhotonNetwork.LoadLevel((int)Define.Scene.GameScene); }
    }
    private IEnumerator LoadFile()
    {
        // 전체 구하기
        totalCount = character + normalMonster + ui;

        for (int i = 0;  i < character; i++)
        {
            // Character 불러오기
            yield return LoadPrefabAsync("Prefabs/Character/" + (Character)i);
        }

        for (int i = 0; i < normalMonster; i++)
        {
            // NormalMonster 불러오기
            yield return LoadPrefabAsync("Prefabs/Monster/Normal/" + (NormalMonster)i);
        }

        // UI 불러오기
        yield return LoadPrefabAsync("Prefabs/UI_Prefabs/" + UI.GameSceneUI);
    }

    private IEnumerator LoadPrefabAsync(string path)
    {
        Main.ResourceManager.Load<GameObject>(path);
        yield return null;

        float progress = (float)loadingCount / totalCount;
        LoadingUI(progress);

        loadingCount++;
        Debug.Log(path + " Loaded!!");
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


        //AsyncOperation op = SceneManager.LoadSceneAsync((int)Define.Scene.GameScene);
        //op.allowSceneActivation = false;    

        //while (!op.isDone)
        //{
        //    float progress = op.progress;
            
            

        //    if (progress >= 0.9f)
        //    {
        //        yield return new WaitForSeconds(1f);
        //        // 씬을 활성화
        //        op.allowSceneActivation = true;
        //    }
        //    yield return null;
        //}
    }
    
}
