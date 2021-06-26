using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    #region texts
    public Text enemyCountText;
    public Text tutorialText;
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
            Invoke("CloseTutorial",3);
        });
        restartButton.onClick.AddListener(() => { OnRestartButton(); });
        nextButton.onClick.AddListener(() => { OnNextButton(); });

        GameManager.instance.levelFinish += LevelFinsih;


        ChangePanel(menuPanel);
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

}
