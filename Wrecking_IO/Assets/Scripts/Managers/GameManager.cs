using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    int totalEnemy = 0;
    bool canPlay = false;
    public int totalEnemies
    {
        get
        {
            return totalEnemy;
        }
        set
        {
            totalEnemy = value;
            uiManager.EnemyText(totalEnemies);
        }
    }
    public Level level;
    #region Events
    public event Action gameStart;
    public event Action<bool> levelFinish;
    #endregion


    public int enviromentShrinks;
    public static int levelNo = 1;
    private void Awake()
    {
        instance = this;
        //enviromentShrinks = level.islandPhases.Count;
    }
    private void Start()
    {
        if (levelNo > level.gameSetting.LevelSettings.Count)
        {
            levelNo = 1;
        }
        level.CreateLevel(levelNo);

    }
    public void GameStart()
    {
        canPlay = true;
        gameStart?.Invoke();
            timeevi = eviromentShrinkTime;
      //  SupplyDrop();
    }


    public float eviromentShrinkTime;
    float timeevi = 0;
    float timeDrop = 0;
    public float reduceScale;
    public float SupplyDropTime;
    void Update()
    {
        if (canPlay)
        {
            timeevi -= Time.deltaTime;
            timeDrop += Time.deltaTime;
            uiManager.AreaShrinkTimer((int)timeevi);

            if (timeDrop > SupplyDropTime)
            {

                SupplyDrop();
                timeDrop = 0;
            }

            if (timeevi < 0)
            {
                uiManager.WarningText("Enviroment Shrinking");
                ShrinkEviroment();
                timeevi = eviromentShrinkTime;
            }

        }
    }

    void SupplyDrop()
    {
        Power pow = Instantiate(level.powerDrop);
        Debug.LogError(level.islandPhases.Count);
        int index1 = UnityEngine.Random.Range(0, level.islandPhases[enviromentShrinks].sideObjects.Count);
        int index2 = UnityEngine.Random.Range(enviromentShrinks+1, level.islandPhases.Count);
        pow.instantiatPos = level.islandPhases[index2].sideObjects[index1].transform.position;
        pow.INIT();
    }

    void ShrinkEviroment()
    {
        StartCoroutine(iShrinkEviroment());
    }
    IEnumerator iShrinkEviroment()
    {
        if (enviromentShrinks < level.islandPhases.Count)
        {
            enviromentShrinks++;
            level.islandcollider.transform.DOScaleX(level.islandcollider.transform.localScale.x - reduceScale, 0);
            level.islandcollider.transform.DOScaleZ(level.islandcollider.transform.localScale.z - reduceScale, 0);
            for (int i = 0; i < level.islandPhases[enviromentShrinks - 1].sideObjects.Count; i++)
            {
                level.islandPhases[enviromentShrinks - 1].sideObjects[i].transform.DOLocalMoveY(-50, 10);
                yield return new WaitForSeconds(.05f);
            }
        }
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
            StartCoroutine(WaitAndExecute(1, () => { LevelFinsih(true); }));

        }
    }

    IEnumerator WaitAndExecute(float wait, Action action)
    {
        yield return new WaitForSeconds(wait);
        action();
    }
}
