using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Util
{
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object  // 컴포넌트 용
    {
        if (go == null)
            return null;

        if (recursive == false) //직속 자식만 찾는 경우
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                //현재기준으로 go는 Canvas
                //1.너가 넣어준 T 타입을 가지고 있다면
                //2.T타입을 가지고 찾은놈의 이름과 우리가 enum에서 기입한 이름이 일치하는지 찾겠다
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>(true))
            {
                //이름이 비어있거나 찾는 이름이면
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)// 컴포넌트 형식이아닌 GameObject 형식 전용의 FindChild
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        return component;
    }
    public static void ShowAlert(string alertMessage, Transform parent = null)
    {
        GameObject alertBlock = Main.ResourceManager.Instantiate("UI_Prefabs/AlertBlock", parent);
        alertBlock.SetActive(true);
        Transform alertGroup = alertBlock.transform.Find("AlertImage");
        alertGroup.Find("AlertMessage").GetComponent<TMP_Text>().text = alertMessage;
        alertGroup.Find("RecognizeBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Main.UIManager.CloseCurrentPopup(alertBlock);
        });
        Main.UIManager.OpenPopup(alertBlock);
    }
}
