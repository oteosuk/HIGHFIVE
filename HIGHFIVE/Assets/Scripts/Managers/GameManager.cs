using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public string SelectedCharacter { get; set; } = "전사";
    public Define.Camp SelectedCamp { get; set; }
    public Character SpawnedCharacter { get; set; }
    private Texture2D _normalTexture;


    public void GameInit()
    {
        _normalTexture = Main.ResourceManager.Load<Texture2D>("Sprites/Cursor/Normal");
        Cursor.SetCursor(_normalTexture, new Vector2(_normalTexture.width / 5, _normalTexture.height / 10), CursorMode.Auto);
    }
}


