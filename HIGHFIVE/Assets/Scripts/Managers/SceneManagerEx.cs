using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void LoadScene(Define.Scene type)
    {
        SceneManager.LoadScene((int)type);
    }
}