using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] CastleLogic castleLogic;
    [SerializeField] GameObject RestartButton;
    private int thisSceneIndex;

    private void Start()
    {
        Time.timeScale = 1;
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void WinGame()
    {
        DisableRestartButton();
        WinPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void LoseGame()
    {
        DisableRestartButton();
        LosePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CheckEnemies()
    {
        EnemyMovement[] enemyArray = GameObject.FindObjectsOfType<EnemyMovement>();
        if ((enemyArray.Length - 1) == 0 && enemySpawner.lastEnemies == 0 && castleLogic.castleHP > 0)
        {
            WinGame();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(thisSceneIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(thisSceneIndex + 1);
        if (SceneManager.sceneCount == thisSceneIndex)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void DisableRestartButton()
    {
        RestartButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }








}
