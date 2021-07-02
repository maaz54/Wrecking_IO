using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    public GameSetting gameSetting;
    public GameObject[] pickupsObjects;
    public GameObject[] enemies;
    public Power powerDrop;
    [System.Serializable]
    public class IslandPhase
    {
        public bool isDown=false;
        public List<GameObject> sideObjects;
    }
    public List<IslandPhase> islandPhases;
public GameObject islandcollider;


    public void CreateLevel(int levelNo)
    {
        PlayerSetting(levelNo);
        CreateEnemies(levelNo);
        // CreatePickupsObjects(levelNo);
        GameManager.instance.totalEnemies = gameSetting.LevelSettings[levelNo-1].enemySetting.Count;
    }
    void PlayerSetting(int levelNo)
    {
        Player.instance.speed = gameSetting.LevelSettings[levelNo-1].speedPlayerSpeed;
        Player.instance.rotSpeed = gameSetting.LevelSettings[levelNo-1].speedRotateSpeed;
    }
    void CreateEnemies(int levelNo)
    {
        for (int i = 0; i < gameSetting.LevelSettings[levelNo-1].enemySetting.Count; i++)
        {
            GameObject go = Instantiate(enemies[gameSetting.LevelSettings[levelNo-1].enemySetting[i].enemyIndex]);
            go.transform.position = gameSetting.LevelSettings[levelNo-1].enemySetting[i].position;
            go.transform.GetChild(0).GetComponent<Enemy>().speed=gameSetting.LevelSettings[levelNo-1].enemySetting[i].Speed;
        }
    }
    void CreatePickupsObjects(int levelNo)
    {
        for (int i = 0; i < gameSetting.LevelSettings[levelNo-1].objectsSetting.Count; i++)
        {
            GameObject go = Instantiate(pickupsObjects[gameSetting.LevelSettings[levelNo-1].objectsSetting[i].ObjectIndex]);
            go.transform.position = gameSetting.LevelSettings[levelNo-1].objectsSetting[i].position;
        }
    }

}
