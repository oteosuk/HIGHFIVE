using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    [SerializeField] private HIGHFIVE_Data HIGHFIVE_data;
    private CharacterDBEntity warrior;
    protected override void Init()
    {
        base.Init();

        DebugTest();
    }

    private void DebugTest()
    {
        // 직접 엑셀 데이터를 인스펙터창에 대입하여(HIGHFIVE_data에) 출력하는 방식(엑셀파일말고 SO형식인 HIGHFIVE_Data.asset을 넣어줘야함)
        Debug.Log(HIGHFIVE_data.Characters[0].job); //전사 출력

        // DataManager에서 만들어둔 CharacterDict 에서 "전사"key의 value( (전사), (전사정보), atk...)를 불러와서 warrior변수에 담아준다.(DataManager에서 확인해볼것)
        // 이 방식을 쓰면 위에 HIGHFIVE_data가 이미 DataManager 에 있으므로 위에 선언해줄필요가 없음
        // (참고)TryGetValue는 bool값을 반환한다.
        Main.DataManager.CharacterDict.TryGetValue("전사", out warrior); 
        Main.DataManager.CharacterDict.TryGetValue("도적", out CharacterDBEntity rogue); // 이렇게 out에서 바로 변수 선언해줄수도있음
        Debug.Log(warrior.def);
        Debug.Log(rogue.job);
    }
}
