using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject settingsMenu;
    public GameObject endgameMenu;
    public TextMeshProUGUI results;

    void Start()
    {
        if(PlayerPrefs.GetInt("GameOver") == 1)
        {
            results.text = PlayerPrefs.GetInt("Time") + "\n" + PlayerPrefs.GetInt("TombstonesDestroyed") + "\n" + PlayerPrefs.GetInt("ZombiesKilled");
            GameOver();
            PlayerPrefs.SetInt("GameOver", 0);
        }
    }

    void Update()
    {
    }

    public void HomeMenu()
    {
        homeMenu.SetActive(true);
        settingsMenu.SetActive(false);
        endgameMenu.SetActive(false);
    }

    public void GameOver()
    {
        homeMenu.SetActive(false);
        settingsMenu.SetActive(false);
        endgameMenu.SetActive(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings()
    {
        homeMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
