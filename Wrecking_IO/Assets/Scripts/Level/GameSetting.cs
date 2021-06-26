using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="GameEconomy",menuName ="ScriptableObject/GameSetting",order =1)]
public class GameSetting : ScriptableObject
{
public List<LevelSetting> LevelSettings;
}
