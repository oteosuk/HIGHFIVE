using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedCharacterStats : UIBase
{
    private enum Texts
    {
        LevelValue,
        ATKValue,
        DEFValue,
        SPDValue
    }

    private TMP_Text _attackValue;
    private TMP_Text _defenceValue;
    private TMP_Text _levelValue;
    private TMP_Text _speedValue;

    void Start()
    {
        Bind<TMP_Text>(typeof(Texts), true);
        CharacterStat spawnedCharacterStat = Main.GameManager.SpawnedCharacter.GetComponent<CharacterStat>();
        StatController statController = spawnedCharacterStat._statController;

        _attackValue = Get<TMP_Text>((int)Texts.ATKValue); 
        _defenceValue = Get<TMP_Text>((int)Texts.DEFValue);
        _levelValue = Get<TMP_Text>((int)Texts.LevelValue);
        _speedValue = Get<TMP_Text>((int)Texts.SPDValue);

        _attackValue.text = spawnedCharacterStat.Attack.ToString();
        _defenceValue.text = spawnedCharacterStat.Defence.ToString();
        _levelValue.text = spawnedCharacterStat.Level.ToString();
        _speedValue.text = spawnedCharacterStat.MoveSpeed.ToString();

        statController.attackChangeEvent += RenewalAttack;
        statController.defenceChangeEvent += RenewalDefence;
        statController.levelChangeEvent += RenewalLevel;
        statController.moveSpeedChangeEvent += RenewalMoveSpeed;
    }

    private void RenewalAttack(int attack)
    {
        _attackValue.text = attack.ToString();
    }
    private void RenewalDefence(int defence)
    {
        _defenceValue.text = defence.ToString();
    }
    private void RenewalLevel(int level)
    {
        _levelValue.text = level.ToString();
    }
    private void RenewalMoveSpeed(float speed)
    {
        _speedValue.text = speed.ToString("N2");
    }
}
