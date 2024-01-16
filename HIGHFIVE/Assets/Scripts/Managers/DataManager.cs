using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DataManager
{
    private bool isInit = false;
    private HIGHFIVE_Data _highFiveData;
    
    // 예를 들어 <"전사", ("전사", "전사정보", atk, def...)>같은 것들이 들어가게될 딕셔너리 타입의 변수 'CharacterDict' //new();도 가능
    public Dictionary<string, CharacterDBEntity> CharacterDict { get; private set; } = new Dictionary<string, CharacterDBEntity>(); //get읽기 set쓰기(수정한다)
    public Dictionary<string, MonsterDBEntity> MonsterDict { get; private set; } = new Dictionary<string, MonsterDBEntity>();
    public Dictionary<string, SkillDBEntity> SkillDict { get; private set; } = new Dictionary<string, SkillDBEntity>();
    public Dictionary<int, ItemDBEntity> ItemDict { get; private set; } = new Dictionary<int, ItemDBEntity>();

    public void Initialize()
    {
        if (isInit) return;
        _highFiveData = Resources.Load<HIGHFIVE_Data>("Data/HIGHFIVE_Data"); //엑셀파일(HIGHFIVE_Data.xlsx)이 아닌 SO파일(HIGHFIVE_Data.asset)이 들어감

        // 캐릭터의 정보를 직업별로 CharacterDict에 넣어준다. (전사,(전사, 전사정보, ..), (도적,(도적, 도적정보, ..)), ... 이런식
        AddEntitiesToDictionary(_highFiveData.Characters, CharacterDict, character => character.job);
        AddEntitiesToDictionary(_highFiveData.Monsters, MonsterDict, monster => monster.name);
        AddEntitiesToDictionary(_highFiveData.Skills, SkillDict, skill => skill.name);
        AddEntitiesToDictionary(_highFiveData.Items, ItemDict, item => item.id);

        isInit = true;
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
