using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TournamentDetail : MonoBehaviour
{
    public string Tournamentid;
    public Text TournamentName_Text;
    public Text TimerHour_Text;
    public Text TimerMinutes_Text;
    public Text TimerSeconds_Text;
    public Text TournamentDuration_Text;
    public TimeSpan Timer;
    public double RemainingTimeSeconds;
    public double RemainingTimeMinutes;
    public double RemainingTimeHours;
    public GameObject TimerImage;
    public GameObject JoinButton;

    void Start()
    {
        //InvokeRepeating("MinusTime", 1f, 1f);
        //Debug.Log(Timer);
        //RemainingTimeSeconds = Timer.Seconds;
        //RemainingTimeMinutes = Timer.Minutes;
        //RemainingTimeHours = Timer.Hours;
    }

    public void InvokeMinusTime()
    {
        InvokeRepeating("MinusTime", 1f, 1f);
        Debug.Log(Timer);
        RemainingTimeSeconds = Timer.Seconds;
        RemainingTimeMinutes = Timer.Minutes;
        RemainingTimeHours = Timer.Hours;
    }

    public void MinusTime()
    {
        if (RemainingTimeSeconds > 0)
        {
            RemainingTimeSeconds = RemainingTimeSeconds - 1;
        }
        else
        {
            if (RemainingTimeMinutes > 0)
            {
                RemainingTimeMinutes = RemainingTimeMinutes - 1;
                RemainingTimeSeconds = 60;
            }
            else
            {
                if (RemainingTimeHours > 0)
                {
                    RemainingTimeHours = RemainingTimeHours - 1;
                    RemainingTimeMinutes = 60;
                }
                else
                {
                    //this.GetComponent<Button>().interactable = true;
                    TimerImage.SetActive(false);
                    JoinButton.SetActive(true);
                    UISelectionManager.instance.TournamentLoadingImage.SetActive(false);
                    UISelectionManager.instance.TournamentScrollView.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        //Timer_Text.text = ( ((int)RemainingTimeSeconds/60)/60).ToString()+"h"+ ((int)RemainingTimeSeconds / 60).ToString()+ "m" + ((int)RemainingTimeSeconds %60).ToString()+"s";
        //Timer_Text.text = RemainingTimeHours.ToString("00")+":"+RemainingTimeMinutes.ToString("00")+":"+RemainingTimeSeconds.ToString("00");
        TimerHour_Text.text = RemainingTimeHours.ToString("00");
        TimerMinutes_Text.text = RemainingTimeMinutes.ToString("00");
        TimerSeconds_Text.text = RemainingTimeSeconds.ToString("00");
    }

    public void OnButtonClick()
    {
        GameConstants.JoinedTournamentId = Tournamentid;
        UISelectionManager.instance.EnvironmentsSelectionPanel.SetActive(true);
    }
}
