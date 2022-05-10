using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WebglMetamask : MonoBehaviour
{
    public static WebglMetamask instance;
    public Text tokentext;

    [DllImport("__Internal")]
    private static extern void getTokenUnity();



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

        GetToken();
    }

    public void GetToken()
    {
        getTokenUnity();
    }


    public void OnTokenFetch(string token)
    {
        Debug.Log("Fetched token " + token);
        tokentext.text = token;
    }






}