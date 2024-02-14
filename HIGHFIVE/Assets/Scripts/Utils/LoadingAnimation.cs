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

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    private void LoadFile()
    {
        // 로드할 것들을 불러오잡
        //플레이어 프리펩
        Main.ResourceManager.Load<GameObject>("Prefabs/Character/Mage");
        Debug.Log("Character Load!!");

        // 몬스터 프리펩
        Main.ResourceManager.Load<GameObject>("Prefabs/Monster/Tree");
        Debug.Log("Monster Load!!");
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync((int)Define.Scene.GameScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            loadingBar.fillAmount = progress;

            if (progress >= 0.9f)
            {
                LoadFile();
                yield return new WaitForSeconds(1f);
                op.allowSceneActivation = true;
            }


            yield return null;
        }
    }
    
}
