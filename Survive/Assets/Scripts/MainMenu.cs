using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject homeMenu;
    public GameObject settingsMenu;
    public GameObject endgameMenu;
    public TextMeshProUGUI results;
    public Slider effectsVolumeSlider;
    public Slider musicVolumeSlider;

    void Start()
    {
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1.0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        if(GameManager.gameOver)
        {
            results.text = (int)GameManager.timeElapsed + "s\n" + GameManager.tombstonesDestroyed + "\n" + GameManager.zombiesKilled;
            GameOver();

            GameManager.ResetValues();
        }
        
        GameManager.UpdateMusicVolume();
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

    public void MusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        GameManager.UpdateMusicVolume();
    }

    public void EffectsVolume()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
