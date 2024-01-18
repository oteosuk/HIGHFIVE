using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private GameObject _player1;
    public void Spawn()
    {
        _player1 = GameObject.Find("Player1");
        

        Debug.Log(_player1.transform); 
        
        Main.ResourceManager.Instantiate("Players/InGamePlayer", _player1.transform);

        GameObject go = Main.ResourceManager.Instantiate("Players/HpBar", _player1.transform);


        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        Destroy(this);
    }
}
