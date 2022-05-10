using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WebglMetamask : MonoBehaviour
{
    public static WebglMetamask instance;

    [DllImport("__Internal")]
    private static extern void getWalletAddressUnity();

    [DllImport("__Internal")]
    private static extern void getBalanceUnity(string walletAddress);

    [DllImport("__Internal")]
    private static extern void stakeUnity(string amount);
    [DllImport("__Internal")]
    private static extern void getStakedBalanceUnity(string adresss);

    [DllImport("__Internal")]
    private static extern void retrieveStakeUnity();

    [HideInInspector]
    public string _walletaddress;
    [HideInInspector]
    public string _Userbalance;

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
        CallgetWalletAddressUnity();
    }

    public void CallgetWalletAddressUnity()
    {
        Debug.Log("Getting wallet address");
        getWalletAddressUnity();
    }
    public void CallgetBalanceUnity(string walletAddress)
    {
        getBalanceUnity(walletAddress);
       // CallgetStakedBalanceUnity();
    }
    public void CallstakeUnity(string Amount)
    {
        //Debug.Log(Amount + "amount sent to web");
        stakeUnity(Amount);
    }

    public void OnWalletAddressFetch(string address)
    {
        _walletaddress = address;
        Debug.Log("wallet adddress " + address);
        CallgetBalanceUnity(address);
        SceneManager.LoadScene(1);
    }

    public void OnBalanceFetch(string balance)
    {
        //Debug.Log("User balance " + balance);
        _Userbalance = balance;

    //    if (UIManager.instance != null)
        {
    //        UIManager.instance.CoinsText.text = balance;
        }




    }
    public void OnRankedStaking(string Con)
    {
        //Debug.Log("Stake was " + Con);

        if (Con == "True")
        {
    //        UIManager.instance.WaitingForConfirmationPanel.SetActive(false);

          //  if (UIManager.instance.selectedMatchMode == 2)
            {
      //          UIManager.instance.PVPStakeBattleButton.interactable = true;

            }
           // else
            {
        //        UIManager.instance.StakeBattleButton.interactable = true;

            }
            //Debug.Log("Battle button enabled " );
        }
        else if(Con == "False")
        {
      //      UIManager.instance.WaitingForConfirmationPanel.SetActive(false);
       //     UIManager.instance.ConfirmationFailedPanel.SetActive(true);
        }
        
    }
    public void CallgetStakedBalanceUnity()
    {
        getStakedBalanceUnity(_walletaddress);
    }



    public void OnStakedBalanceFetch(string con)
    {

        //Debug.Log("has stake amount " + con);

        if (con == "True")
        {
      //      UIManager.instance.RetrieveButton.interactable = true;
        }
        else if (con == "False")
        {
       //     UIManager.instance.RetrieveButton.interactable = false;
        }
    }

    public void OnRetrieveStake(string con)
    {

        //Debug.Log("OnRetrieveStake amount " + con);

        if (con == "True")
        {
        //    UIManager.instance.RetrieveButton.interactable = false;
        }
        else if(con == "False")
        {
       //     UIManager.instance.RetrieveButton.interactable = true;
        }
    }
    public void CallretrieveStakeUnity()
    {
        retrieveStakeUnity();
    }
    }
