using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedCharacterStats : MonoBehaviour
{
    private Character _character;

    [SerializeField] private TMP_Text _spawnedcharacterAtk;
    [SerializeField] private TMP_Text _spawnedcharacterDef;
    [SerializeField] private TMP_Text _spawnedcharacterSpd;

    void Start()
    {
        _character = Main.GameManager.SpawnedCharacter;

        _spawnedcharacterAtk.text = _character.stat.Attack.ToString();
        _spawnedcharacterDef.text = _character.stat.Defence.ToString();
        _spawnedcharacterSpd.text = _character.stat.MoveSpeed.ToString();
    }

    void Update()
    {
        
    }
}
