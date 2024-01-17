using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class StartSceneLoginTest : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbzAtZte-8FV1y-CA0iW1ITmofm__kZDQra2doRDbnuk3c-RKMO_UGwAZ31YVk8a1GW2QQ/exec";
    private TMP_InputField _nicknameField;
    private string nickname;

    private void Awake()
    {
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
        nickname = _nicknameField.text;
    }
    
    IEnumerator Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("value", "값");

        //UnityWebRequest www = UnityWebRequest.Get(URL);
        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }

    bool SetIDPass()
    {
        nickname = _nicknameField.text.Trim();

        if (nickname == "") return false;
        else return true;
    }


    public void Register()
    {
        if (!SetIDPass())
        {
            print("아이디 또는 비밀번호가 비어있습니다");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("nickname", nickname);

        //StartCoroutine(Post(form));
    }

    /*IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }
    }*/

    public void OnButtonClick()
    {
        Debug.Log(Main.DataManager.CharacterDict.Count);
        //Debug.Log(warrior.def);
        Main.NetworkManager.Connect(_nicknameField.text);
    }
}
