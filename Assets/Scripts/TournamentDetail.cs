using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TournamentDetail : MonoBehaviour
{
    public Text TournamentName_Text;
    public Text Timer_Text;
    public Text TournamentDuration_Text;
    public TimeSpan Timer;
    public double RemainingTimeSeconds;
    public double RemainingTimeMinutes;
    public double RemainingTimeHours;

    void Start()
    {
        InvokeRepeating("MinusTime", 1f, 1f);
        RemainingTimeSeconds = Timer.Seconds;
        RemainingTimeMinutes = Timer.Minutes;
        RemainingTimeHours = Timer.Hours;
    }

    public void MinusTime()
    {
        if (RemainingTimeSeconds < 0)
        {
            RemainingTimeSeconds = RemainingTimeSeconds + 1;
        }
        else
        {
            if (RemainingTimeMinutes < 0)
            {
                RemainingTimeMinutes = RemainingTimeMinutes + 1;
                RemainingTimeSeconds = -60;
            }
            else
            {
                if (RemainingTimeHours < 0)
                {
                    RemainingTimeHours = RemainingTimeHours + 1;
                    RemainingTimeMinutes = -60;
                }
            }
        }
    }

    private void Update()
    {
        //Timer_Text.text = ( ((int)RemainingTimeSeconds/60)/60).ToString()+"h"+ ((int)RemainingTimeSeconds / 60).ToString()+ "m" + ((int)RemainingTimeSeconds %60).ToString()+"s";
        Timer_Text.text = (int)RemainingTimeHours + "h" + (int)RemainingTimeMinutes + "m" + (int)RemainingTimeSeconds + "s";
    }
}
