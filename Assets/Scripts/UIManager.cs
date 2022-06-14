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

    //For Sounds
    public Button[] GameplayButtons;

    public GameObject[] PowerupsButtons;
    public GameObject[] PowerUpTimerImage;
    public GameObject[] PowerUpTimerFillImage;
    private int PowerUpRefreshTime = 10;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        foreach (Button b in GameplayButtons)
        {
            b.onClick.AddListener(SoundManager.instance.OnButtonClick);
        }
        if (GameConstants.GameType == "Tournament")
        {
            GameplayButtons[5].gameObject.SetActive(false);
        }

        PowerupsButtons[GameConstants.SelectedPowerupNumber].SetActive(true);
        PowerupsButtons[0].SetActive(false);
        CallPowerUpRefreshTimer();
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
        GameConstants.IsPaused = false;
        SceneManager.LoadScene(GameConstants.SceneLoaded);
    }

    public void OnHomeButton()
    {
        if (GameConstants.GameType == "Tournament")
        {
            LeaderboardPanel.SetActive(true);
            GameOverPanel.SetActive(false);
            APIManager.instance.GetLeaderboardAPI(LeaderboardScrollContent);
            Time.timeScale = 1;
            GameConstants.IsPaused = false;
        }
        else
        {
            Time.timeScale = 1;
            GameConstants.IsPaused = false;
            SceneManager.LoadScene(0);
        }
    }

    public void OnLeaderBoardCloseButton()
    {
        SceneManager.LoadScene(0);
    }

    public void CallPowerUpRefreshTimer()
    {
        StartCoroutine(PowerUpRefreshTimer());
    }
    public IEnumerator PowerUpRefreshTimer()
    {
        PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable = false;
        PowerUpTimerImage[GameConstants.SelectedPowerupNumber].SetActive(true);
        PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount = 0;

        while (PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount < 1)
        {
            PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount += 0.066f / 8;
            yield return new WaitForSeconds(1f / 8);
        }

        PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable = true;
        PowerUpTimerImage[GameConstants.SelectedPowerupNumber].SetActive(false);
        PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount = 0;
        StopCoroutine(PowerUpRefreshTimer());
    }


    public void CallPowerUpDurationTimer()
    {
        StartCoroutine(PowerUpDurationTimer());
    }
    public IEnumerator PowerUpDurationTimer()
    {
        PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable = false;
        PowerUpTimerImage[GameConstants.SelectedPowerupNumber].SetActive(true);
        PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount = 1;

        while (PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount > 0)
        {
            PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount -= 0.1f / 8;
            yield return new WaitForSeconds(1f / 8);
        }

        PowerUpTimerImage[GameConstants.SelectedPowerupNumber].SetActive(false);
        PowerUpTimerFillImage[GameConstants.SelectedPowerupNumber].GetComponent<Image>().fillAmount = 0;
        if (GameConstants.SelectedPowerupNumber != 0)
        {
            CallPowerUpRefreshTimer();
        }
        else
        {
           PowerupsButtons[0].SetActive(false);
        }
    }

    public void CallPowerUpInvisibilityDurationTimer()
    {
        PowerupsButtons[0].GetComponent<Button>().interactable = false;
        PowerUpTimerImage[0].SetActive(false);
        PowerUpTimerFillImage[0].GetComponent<Image>().fillAmount = 0;
        StopCoroutine(PowerUpRefreshTimer());
        
        StartCoroutine(PowerUpInvisibilityDurationTimer());
    }
    public IEnumerator PowerUpInvisibilityDurationTimer()
    {
        PowerupsButtons[0].GetComponent<Button>().interactable = false;
        PowerUpTimerImage[0].SetActive(true);
        PowerUpTimerFillImage[0].GetComponent<Image>().fillAmount = 1;

        while (PowerUpTimerFillImage[0].GetComponent<Image>().fillAmount > 0)
        {
            PowerUpTimerFillImage[0].GetComponent<Image>().fillAmount -= 0.1f / 8;
            yield return new WaitForSeconds(1f / 8);
        }

        PowerUpTimerImage[0].SetActive(false);
        PowerUpTimerFillImage[0].GetComponent<Image>().fillAmount = 0;
        PowerupsButtons[0].SetActive(false);
        StartCoroutine(PowerUpRefreshTimer());
        StopCoroutine(PowerUpInvisibilityDurationTimer());
        
    }


}
