using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DataManager
{
    private HIGHFIVE_Data _highFiveData;

    public Dictionary<string, CharacterDBEntity> CharacterDict { get; private set; } = new Dictionary<string, CharacterDBEntity>();
    public Dictionary<string, MonsterDBEntity> MonsterDict { get; private set; } = new Dictionary<string, MonsterDBEntity>();
    public Dictionary<string, SkillDBEntity> SkillDict { get; private set; } = new Dictionary<string, SkillDBEntity>();
    public Dictionary<int, ItemDBEntity> ItemDict { get; private set; } = new Dictionary<int, ItemDBEntity>();

    public void Initialize()
    {
        _highFiveData = Resources.Load<HIGHFIVE_Data>("Data/HIGHFIVE_Data");

        AddEntitiesToDictionary(_highFiveData.Characters, CharacterDict, character => character.job);
        AddEntitiesToDictionary(_highFiveData.Monsters, MonsterDict, monster => monster.name);
        AddEntitiesToDictionary(_highFiveData.Skills, SkillDict, skill => skill.name);
        AddEntitiesToDictionary(_highFiveData.Items, ItemDict, item => item.id);
    }

    private void AddEntitiesToDictionary<T>(List<T> entities, Dictionary<string, T> dictionary, Func<T, string> keySelector)
    {
        foreach (var entity in entities)
        {
            dictionary.Add(keySelector(entity), entity);
        }
    }

    private void AddEntitiesToDictionary<T>(List<T> entities, Dictionary<int, T> dictionary, Func<T, int> keySelector)
    {
        foreach (var entity in entities)
        {
            dictionary.Add(keySelector(entity), entity);
        }
    }
}
