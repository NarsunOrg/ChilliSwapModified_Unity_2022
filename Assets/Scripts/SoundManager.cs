using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Button[] MainmenuButtons;
    public AudioClip ButtonsClip;
    public AudioClip GreenChilliClip;
    public AudioClip RedChilliClip;
    public AudioClip BlueChilliClip;
    public AudioClip GoldenChilliClip;
    public AudioClip PowerUpClip;
    public AudioClip GirlDeathClip;
    public AudioClip BoyDeathClip;
    public AudioClip GirlJumpClip;
    public AudioClip BoyJumpClip;
    public AudioSource AS;
    public AudioSource ASPlayer;
    public AudioSource ASChillies;
    public AudioSource ASBg;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        foreach (Button b in MainmenuButtons)
        {
            b.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        AS.clip = ButtonsClip;
        AS.Play();
    }
}
