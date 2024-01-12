using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Main.SceneManagerEx.LoadScene(Define.Scene.StartScene);
            Debug.Log("마우스 좌클릭!");
        }
    }
}
