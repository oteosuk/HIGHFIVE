using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTest : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Main.SceneManagerEx.LoadScene(Define.Scene.StartScene);
        }
    }
}
