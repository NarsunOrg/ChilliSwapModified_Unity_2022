using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject PausedPanel;
    public GameObject GameOverPanel;
    public GameObject LeaderboardPanel;
    public Transform LeaderboardScrollContent;
    public GameObject HomeButton;
    public GameObject RestartButton;
    public GameObject GameOverHomeButton;

    //For Gameplay text fields
    public Text ChilliCountText;
    public Text PlayerCurrentLiveText;
    public Text PlayerTotalTimeHourText;
    public Text PlayerTotalTimeMinuteText;
    public Text PlayerTotalTimeSecondsText;
    public Text PlayerTotalDistanceCoveredText;

    //For PausedPanel Text fields
    public Text PausedPanelChilliCountText;
    public Text PausedPanelTimeHourText;
    public Text PausedPanelTimeMinuteText;
    public Text PausedPanelTimeSecondsText;
    public Text PausedPanelDistanceCoveredText;

    //For GameOverPanel Text fields
    public Text GameOverPanelChilliCountText;
    public Text GameOverPanelTimeHourText;
    public Text GameOverPanelTimeMinuteText;
    public Text GameOverPanelTimeSecondsText;
    public Text GameOverPanelDistanceCoveredText;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        ChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        PlayerCurrentLiveText.text = GameManager.instance.CurrentLives.ToString();
        //PlayerTotalTimeSpendText.text = GameManager.instance.TotalTimeSpend.ToString();
        PlayerTotalTimeHourText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Hours).ToString("00");
        PlayerTotalTimeMinuteText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Minutes).ToString("00");
        PlayerTotalTimeSecondsText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Seconds).ToString("00");
        PlayerTotalDistanceCoveredText.text = (GameManager.instance.TotalDIstanceCovered / 10000).ToString();
    }

    public void OnPausedButton()
    {
        GameConstants.IsPaused = true;
        Time.timeScale = 0;
        PausedPanelChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        PausedPanelTimeHourText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Hours).ToString("00");
        PausedPanelTimeMinuteText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Minutes).ToString("00");
        PausedPanelTimeSecondsText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Seconds).ToString("00");
        PausedPanelDistanceCoveredText.text = (GameManager.instance.TotalDIstanceCovered / 10000).ToString();
        PausedPanel.SetActive(true);
    }

    public void OnReplayButton()
    {
        Time.timeScale = 1;
        GameConstants.IsPaused = false;
        PausedPanel.SetActive(false);
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameConstants.SceneLoaded);
    }

    public void OnHomeButton()
    {
        if (GameConstants.GameType == "Tournament")
        {
            LeaderboardPanel.SetActive(true);
            APIManager.instance.GetLeaderboardAPI(LeaderboardScrollContent);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OnLeaderBoardCloseButton()
    {
        SceneManager.LoadScene(0);
    }
}
