using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text ChilliCountText;
    public Text PlayerCurrentLiveText;

    public void Update()
    {
        ChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        PlayerCurrentLiveText.text = GameManager.instance.CurrentLives.ToString();
    }
}
