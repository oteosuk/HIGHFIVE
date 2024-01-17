using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private GameObject spawnPoint;
    public void Spawn() 
    {
        spawnPoint = GameObject.Find("Player1");
                
        GameObject _player1 =  Main.ResourceManager.Instantiate("Players/InGamePlayer", spawnPoint.transform);
        GameObject go = Main.ResourceManager.Instantiate("Players/HpBack", _player1.transform);

        Canvas canvas = go.GetComponent<Canvas>();

        if (canvas == null)
        {
            canvas = go.AddComponent<Canvas>();
        }

        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        Destroy(this);
    }

}

