using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHittingHurdle : MonoBehaviour
{
    public PlayerController PC;
    public GameObject restartPanel;

    private void Start()
    {
        PC.MonsterMovement(1);
        StartCoroutine("StumbleWait");
    }

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Hurdle")
        {
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool)
            {

                if (PC.AlreadyHit)
                {
                    PC.MonsterMovement(2);
                    if (PC.dead == false && PC.PortalUse == false)
                    {
                        PC.PlayerRespawnTransform.SetParent(null);
                        PC.dead = true;
                        PC.MonsterAttackAnim();
                        //PC.CancelFunctionsInvoke();
                        PC.PlayerAnim.SetBool("Running", false);
                        PC.PlayerAnim.SetBool("Death", true);
                        
                        GameManager.instance.CurrentLives -= 1;
                        if (GameManager.instance.CurrentLives < 1)
                        {
                            Invoke("LoadSceneDelayCall", 3f);   //on after this chus of restart
                            //Invoke("PanelDelayCall", 3f);
                        }
                        else
                        {
                            PC.DisablePowerUps();
                            Invoke("RespawnPlayerDelayCall", 3f);    //on after this chus of restart
                            //Invoke("PanelDelayCall", 3f);
                        }
                    }
                  
                }
                if (!PC.AlreadyHit)
                {
                    PC.PlayerAnim.SetTrigger("Stumble");
                    PC.PlayerAnim.SetBool("Sliding", false);
                    PC.MonsterMovement(1);
                    StartCoroutine("StumbleWait");
                    
                }

            }
        }

        if (other.gameObject.tag == "HurdleDie")
        {
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool)
            {
                PC.MonsterMovement(2);
                if (PC.dead == false && PC.PortalUse == false)
                {
                    PC.PlayerRespawnTransform.SetParent(null);
                    PC.dead = true;
                    PC.MonsterAttackAnim();
                    //PC.CancelFunctionsInvoke();
                    PC.PlayerAnim.SetBool("Running", false);
                    PC.PlayerAnim.SetBool("Death", true);
                    GameManager.instance.CurrentLives -= 1;
                    if (GameManager.instance.CurrentLives < 1)
                    {
                        Invoke("LoadSceneDelayCall", 3f);    //on after this chus of restart
                        //Invoke("PanelDelayCall", 3f);
                    }
                    else
                    {
                        PC.DisablePowerUps();
                        Invoke("RespawnPlayerDelayCall", 3f);     //on after this chus of restart
                        //Invoke("PanelDelayCall", 3f);
                    }
                }
               
            }
        }

        if (other.gameObject.tag == "MachineHurdle")
        {
            other.transform.parent.gameObject.SetActive(false);
        }
    }

    IEnumerator StumbleWait()
    {
        yield return new WaitForSeconds(2);
        PC.AlreadyHit = true;
        yield return new WaitForSeconds(7);
        PC.AlreadyHit = false;
        PC.MonsterMovement(0);
    }
    
    public void RespawnPlayerDelayCall()
    {
        PC.RespwanPlayer();
        PC.MonsterMovement(1);
        StartCoroutine("StumbleWait");
    }

    public void PanelDelayCall()
    {
        restartPanel.SetActive(true);
    }

    public void LoadSceneDelayCall()
    {
        UIManager.instance.GameOverPanelChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        UIManager.instance.GameOverPanelTimeHourText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Hours).ToString("00");
        UIManager.instance.GameOverPanelTimeMinuteText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Minutes).ToString("00");
        UIManager.instance.GameOverPanelTimeSecondsText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Seconds).ToString("00");
        UIManager.instance.GameOverPanelDistanceCoveredText.text = (GameManager.instance.TotalDIstanceCovered / 10000).ToString();
        UIManager.instance.GameOverPanel.SetActive(true);
        //SceneManager.LoadScene(0);
    }
}
