using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public string SelectedCharacter { get; set; } = "전사";
    public Define.Camp SelectedCamp { get; set; }

    public Character SpawnObj { get; set; }

}


