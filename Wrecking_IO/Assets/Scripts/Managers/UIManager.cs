using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{

    #region texts
    public Text enemyCountText;
    public Text AreaShrinkTimerText;
    public Text tutorialText;
    public Text warningText;

    #endregion

    #region  Buttons
    public Button playButton;
    public Button restartButton;
    public Button nextButton;
    #endregion

    #region Panels
    public GameObject menuPanel;
    public GameObject levelCompletePanel;
    public GameObject levelFailPanel;
    public GameObject gamePlayPanel;

    #endregion
    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            ChangePanel(gamePlayPanel);
            GameManager.instance.GameStart();
            Invoke("CloseTutorial", 3);
        });
        restartButton.onClick.AddListener(() => { OnRestartButton(); });
        nextButton.onClick.AddListener(() => { OnNextButton(); });

        GameManager.instance.levelFinish += LevelFinsih;


        ChangePanel(menuPanel);
    }

public void AreaShrinkTimer(int time)
{
AreaShrinkTimerText.text ="Area Start Shrinking in " + time;
}
    public void WarningText(string msg)
    {
        warningText.gameObject.SetActive(true);
        warningText.text = msg;
        StartCoroutine(WaitAndExecute(1, () => { warningText.gameObject.SetActive(false); }));
    }

    void CloseTutorial()
    {
        tutorialText.gameObject.SetActive(false);
    }

    void ChangePanel(GameObject panel)
    {
        menuPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        levelFailPanel.SetActive(false);
        gamePlayPanel.SetActive(false);


        panel.SetActive(true);
    }

    public void EnemyText(int noEnemy)
    {
        enemyCountText.text = "Enemies Remaining: " + noEnemy;
    }
    void LevelFinsih(bool isComplete)
    {
        if (isComplete)
        {
            ChangePanel(levelCompletePanel);
        }
        else
        {
            ChangePanel(levelFailPanel);
        }
    }
    void OnRestartButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void OnNextButton()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    IEnumerator WaitAndExecute(float wait, Action action)
    {
        yield return new WaitForSeconds(wait);
        action();
    }
}
