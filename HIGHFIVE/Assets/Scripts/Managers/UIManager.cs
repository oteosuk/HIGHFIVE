using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem;

public class UIManager
{
    public Stack<GameObject> popUpStack = new Stack<GameObject>();
    public bool isLoading = false;

    public void OpenPopup(GameObject go)
    {
        popUpStack.Push(go);
        go.SetActive(true);
        Debug.Log("dd");
        // 트윈을 순차적으로 실행할 수 있는 시퀀스를 만든다. 
        var set = DOTween.Sequence();

        set.Append(go.transform.DOScale(1.1f, 0.2f));
        set.Append(go.transform.DOScale(1f, 0.1f));

        set.Play();
    }
    public void CloseCurrentPopup()
    {
        if (popUpStack.Count > 0)
        {
            GameObject go = popUpStack.Pop();
            // 트윈을 순차적으로 실행할 수 있는 시퀀스를 만든다. 
            var set = DOTween.Sequence();

            // Append는 시퀀스를 추가하는 데 사용된다.
            set.Append(go.transform.DOScale(1.1f, 0.2f));
            set.Append(go.transform.DOScale(0.2f, 0.2f));

            // set이 시퀀스를 모두 완료하면 => { } 안에 명령어들을 실행한다.
            set.Play().OnComplete(() =>
            {
                go.SetActive(false);
            });
        }
    }

    public T CreateWorldUI<T>(string path, UnityEngine.Transform parent = null) where T : UIBase
    {
        GameObject go = Main.ResourceManager.Instantiate($"UI_World/{path}");
        if (parent != null)
        {
            go.transform.SetParent(parent);
        }

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        canvas.renderMode = RenderMode.WorldSpace;

        return Util.GetOrAddComponent<T>(go);
    }

   public void UIUpdate()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !isLoading)
        {
            CloseCurrentPopup();
        }
    } 
}