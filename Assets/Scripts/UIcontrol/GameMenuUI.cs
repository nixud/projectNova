using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject settingMenu;

    public void PauseButton()
    {
        Time.timeScale = 0;
        gameMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        gameMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
    }

    public void SettingButton()
    {
        gameMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void BackButton()
    {
        settingMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
}
