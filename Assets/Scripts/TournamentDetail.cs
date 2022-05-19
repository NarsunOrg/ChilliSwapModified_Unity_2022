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

    void Start()
    {
        InvokeRepeating("MinusTime", 1f, 1f);



        RemainingTimeSeconds = Timer.TotalSeconds;
    }

    public void MinusTime()
    {
        RemainingTimeSeconds = RemainingTimeSeconds - 1;
        Debug.Log("aaaa" + RemainingTimeSeconds);
        //Timer.Subtract(new TimeSpan(0, 0, 1));
    }
    private void Update()
    {
        Timer_Text.text = ( ((int)RemainingTimeSeconds/60)/60).ToString()+"h"+ ((int)RemainingTimeSeconds / 60).ToString()+ "m" + ((int)RemainingTimeSeconds %60).ToString()+"s";
    }
}
