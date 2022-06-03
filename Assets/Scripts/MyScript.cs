using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MyScript : MonoBehaviour
{

    public static MyScript instance;
    [DllImport("__Internal")]
    private static extern void registerVisibilityChangeEvent();

    public void Awake()
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
    }
    void Start()
    {

        registerVisibilityChangeEvent();
    }

    void OnVisibilityChange(string visibilityState)
    {
        System.Console.WriteLine("[" + System.DateTime.Now + "] the game switched to " + (visibilityState == "visible" ? "foreground" : "background"));
        if (visibilityState == "visible")
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

}