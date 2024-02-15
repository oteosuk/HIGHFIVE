using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingAnimation : MonoBehaviour
{
    public Image loadingBar;
    public TMP_Text loadingTxt;

    private int loadingCount = 0;
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator LoadFile()
    {
        // 혹시 나중에 한번에 불러올 수도 있지 않을까해서 수정 여지가 있음.
        // Character 불러오기
        yield return LoadPrefabAsync("Prefabs/Character/Mage");
        yield return LoadPrefabAsync("Prefabs/Character/Warrior");
        yield return LoadPrefabAsync("Prefabs/Character/Rogue");

        // Monster 불러오기
        yield return LoadPrefabAsync("Prefabs/Monster/Normal/Tree");
        yield return LoadPrefabAsync("Prefabs/Monster/Normal/GreenOrc1");
        yield return LoadPrefabAsync("Prefabs/Monster/Normal/GreenOrc2");

        // UI 불러오기
        yield return LoadPrefabAsync("Prefabs/UI_Prefabs/GameSceneUI");
    }

    private IEnumerator LoadPrefabAsync(string path)
    {
        Main.ResourceManager.Load<GameObject>(path);
        yield return null;

        // 파일의 총 갯수를 구해야하는데 아직 구하지 못해서 일단 하드코딩으로 하고 있음.
        float progress = (float)loadingCount / 7;
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
        AsyncOperation op = SceneManager.LoadSceneAsync((int)Define.Scene.GameScene);
        op.allowSceneActivation = false;    

        while (!op.isDone)
        {
            float progress = op.progress;
            
            yield return LoadFile();

            if (progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                // 씬을 활성화
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    
}
