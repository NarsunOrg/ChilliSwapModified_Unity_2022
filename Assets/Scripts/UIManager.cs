using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text ChilliCountText;
    public Text PlayerCurrentLiveText;
    public Text PlayerTotalTimeSpendText;
    public Text PlayerTotalDistanceCoveredText;

    public void Update()
    {
        ChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        PlayerCurrentLiveText.text = GameManager.instance.CurrentLives.ToString();
        PlayerTotalTimeSpendText.text = GameManager.instance.TotalTimeSpend.ToString();
        PlayerTotalDistanceCoveredText.text = GameManager.instance.TotalDIstanceCovered.ToString();
    }
}
