using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;

    int totalEnemy=0;

    public int totalEnemies
    {
        get{
            return totalEnemy;
        }
        set{
            totalEnemy=value;
             uiManager.EnemyText(totalEnemies);
        }
    }
    public Level level;
    #region Events
    public event Action gameStart;
    public event Action<bool> levelFinish;
    #endregion

    public static int levelNo = 1;
    private void Awake()
    {
        instance = this;
    }
    private void Start() {
        if (levelNo > level.gameSetting.LevelSettings.Count)
        {
            levelNo = 1;
        }
        level.CreateLevel(levelNo);
    }
    public void GameStart()
    {
        
        gameStart?.Invoke();
    }
    public void LevelFinsih(bool isComplete)
    {
        if (isComplete)
        {
            levelNo++;
        }
        levelFinish?.Invoke(isComplete);
    }

    public void EnemyDead()
    {
        totalEnemies--;
       
        if (totalEnemies <= 0)
        {
            StartCoroutine(WaitAndExecute(1,()=>{LevelFinsih(true);}));
            
        }
    }

    IEnumerator WaitAndExecute(float wait,Action action)
    {
        yield return new WaitForSeconds(wait);
        action();
    }
}
