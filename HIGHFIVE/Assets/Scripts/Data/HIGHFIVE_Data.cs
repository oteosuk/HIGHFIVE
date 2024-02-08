using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class HIGHFIVE_Data : ScriptableObject
{
	public List<CharacterDBEntity> Characters;
	public List<SkillDBEntity> Skills;
	public List<MonsterDBEntity> Monsters;
	public List<ItemDBEntity> Items;
	public List<BuffDBEntity> Buff;
}
