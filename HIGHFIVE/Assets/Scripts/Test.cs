using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    private TMP_InputField _nicknameField;

    private void Awake()
    {
        _nicknameField = GameObject.Find("NicknameField").GetComponent<TMP_InputField>();
    }

    public void OnButtonClick()
    {
        Debug.Log("h");
        Main.NetworkManager.Connect(_nicknameField.text);
    }
}