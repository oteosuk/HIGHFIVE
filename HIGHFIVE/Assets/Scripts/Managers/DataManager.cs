using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DataManager
{
    private HIGHFIVE_Data _highFiveData;

    public Dictionary<string, CharacterDBEntity> CharacterDict { get; private set; } = new Dictionary<string, CharacterDBEntity>();

    public void Initialize()
    {
        _highFiveData = Resources.Load<HIGHFIVE_Data>("Data/HIGHFIVE_Data");


        for (int i = 0; i < _highFiveData.Characters.Count; i++)
        {
            CharacterDict.Add(_highFiveData.Characters[i].job, _highFiveData.Characters[i]);
        }
    }
}
