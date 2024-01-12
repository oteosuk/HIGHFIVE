using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class HIGHFIVE_Data : ScriptableObject
{
	public List<CharacterDBEntity> Characters; // Replace 'EntityType' to an actual type that is serializable.
	public List<SkillDBEntity> Skills; // Replace 'EntityType' to an actual type that is serializable.
	public List<MonsterDBEntity> Monsters; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemDBEntity> Items; // Replace 'EntityType' to an actual type that is serializable.
}
