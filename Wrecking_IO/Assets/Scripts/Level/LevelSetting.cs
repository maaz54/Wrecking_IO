using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSetting
{
    public float speedPlayerSpeed;
    public float speedRotateSpeed;
   public List<EnemySetting> enemySetting;
    public List<PickAbleObjects> objectsSetting;
}
[System.Serializable]
public class EnemySetting
{
    /// <summary>
    /// if we use different types of enemy we call it with there index
    /// </summary>
    public int enemyIndex;
    public float Speed;
    public Vector3 position;
}
[System.Serializable]
public class PickAbleObjects
{
    /// <summary>
    /// if we use different types of Object we call it with there index
    /// </summary>
    public int ObjectIndex;
    public Vector3 position;
}