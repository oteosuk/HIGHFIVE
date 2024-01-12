using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DataManager
{
    private HIGHFIVE_Data _highFiveData;
    
    // 예를 들어 <"전사", ("전사", "전사정보", atk, def...)>같은 것들이 들어가게될 딕셔너리 타입의 변수 'CharacterDict'
    public Dictionary<string, CharacterDBEntity> CharacterDict { get; private set; } = new Dictionary<string, CharacterDBEntity>();

    public void Initialize()
    {
        _highFiveData = Resources.Load<HIGHFIVE_Data>("Data/HIGHFIVE_Data"); //엑셀파일(HIGHFIVE_Data.xlsx)이 아닌 SO파일(HIGHFIVE_Data.asset)이 들어감

        // 캐릭터의 정보를 직업별로 CharacterDict에 넣어준다. (전사,(전사, 전사정보, ..), (도적,(도적, 도적정보, ..)), ... 이런식
        for (int i = 0; i < _highFiveData.Characters.Count; i++)
        {
            CharacterDict.Add(_highFiveData.Characters[i].job, _highFiveData.Characters[i]);
        }
    }
}
