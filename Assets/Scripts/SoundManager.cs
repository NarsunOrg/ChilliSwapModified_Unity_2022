using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Button[] MainmenuButtons;
    public AudioClip ButtonsSounds;
    public AudioClip GameplaySound;
    public AudioSource AS;
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
        AS.clip = ButtonsSounds;
        AS.Play();
    }
}
