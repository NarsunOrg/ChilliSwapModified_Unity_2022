﻿using System;
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
        StartCoroutine(StartCall());
    }

    void Update()
    {
        
        

       

    }

    IEnumerator StartCall()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), out hit, 2, LayerMask.GetMask("Col")))
        {
            if (hit.collider.transform.tag == "Wall")
            {
                if (PC.dead == false && PC.PortalUse == false)
                {
                    PC.PlayerRespawnTransform.SetParent(null);
                    PC.dead = true;

                    //if (GameConstants.SelectedPlayerForGame.bodytype == "boy")
                    //{
                    //    SoundManager.instance.ASPlayer.clip = SoundManager.instance.BoyDeathClip;
                    //    SoundManager.instance.ASPlayer.Play();
                    //}
                    //else
                    //{
                    //    SoundManager.instance.ASPlayer.clip = SoundManager.instance.GirlDeathClip;
                    //    SoundManager.instance.ASPlayer.Play();
                    //}
                    PC.MonsterAnim.SetBool("Attack", true);
                    PC.PlayerAnim.SetBool("Running", false);
                    PC.PlayerAnim.SetBool("Death", true);
                    // PC.DisablePowerUps();
                    //  PC.MonsterMovement(2);
                    //GameManager.instance.CurrentLives -= 1;
                    //if (GameManager.instance.CurrentLives < 1)
                    //{
                    //    if (GameConstants.GameType == "Tournament")
                    //    {
                    //        APIManager.instance.PostTournamentResultApi(GameConstants.JoinedTournamentId, (GameManager.instance.TotalDIstanceCovered / 10000).ToString(), GameManager.instance.TotalTimeSpend.ToString(), GameManager.instance.CollectedChillis.ToString());
                    //        Debug.Log(GameConstants.JoinedTournamentId + GameManager.instance.TotalDIstanceCovered + GameManager.instance.TotalTimeSpend + GameManager.instance.CollectedChillis);
                    //    }
                    //    Invoke("LoadSceneDelayCall", 3f);
                    //}
                    //else
                    {
                        PC.DisablePowerUps();
                        // Invoke("RespwanPlayer", 3f);
                        PC.RespwanPlayer_Fn();
                        // StartCoroutine("StumbleWaitWall");
                        // PC.StumbleWaitWall_Fn();
                    }

                }
            }

            if (hit.collider.gameObject.GetComponent<StartPosRefrence>() != null)
            {
                PC.isonturn = true;
                if (PC.SuperSpeedBool)
                {
                    PC.SuperSpeedTurn(hit.collider.gameObject.GetComponent<StartPosRefrence>().NextPosition, hit.collider.gameObject.tag);
                }
                else
                {
                    PC.EnteredPlatformTrigger(hit.collider.gameObject.GetComponent<StartPosRefrence>().NextPosition, hit.collider.gameObject.tag);
                    Debug.Log("Hittingggggggggggggggggggggg :" + hit.collider.gameObject.tag);
                }
            }
            else
            {
                PC.isonturn = false;
            }
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 2, Color.yellow);
            Debug.Log("Did Hit");

        }
        else
        {
            PC.isonturn = false;
            Debug.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * 2, Color.white);
            Debug.Log("Did not Hit");
        }
       
        #region Parent Movement
        float x = PC.Parent.transform.position.x;
        float z = PC.Parent.transform.position.z;

        if (!PC.dead)
        {
            // Parent.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
            //  Parent.transform.localPosition = new Vector3(Parent.transform.localPosition.x, Parent.transform.localPosition.y, Parent.transform.localPosition.z-( speed * Time.deltaTime));
            PC.Parent.transform.localPosition = Vector3.Lerp(PC.Parent.transform.localPosition, (PC.Parent.transform.localPosition + PC.Parent.transform.forward * PC.speed), 0.016f); //PC.Parent.transform.forward * PC.speed * Time.deltaTime;

        }
        if ((PC.gameObject.transform.localPosition.z > 0.0001 || PC.gameObject.transform.localPosition.z < -0.0001) && !PC.dead)
        {
            // gameObject.transform.DOLocalMoveZ(0, 0.01f);
            PC.gameObject.transform.localPosition = new Vector3(PC.gameObject.transform.localPosition.x, PC.gameObject.transform.localPosition.y, 0.01f);

        }
        yield return new WaitForSecondsRealtime(0.016f);
        StartCoroutine(StartCall());
        #endregion
    }

    private void FixedUpdate()
    {
        
        
    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Hurdle")
        {
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool && !PC.RespawnInvisibilityBool)
            {

                if (PC.AlreadyHit)
                {
                    PC.MonsterMovement(2);
                    if (PC.dead == false && PC.PortalUse == false)
                    {
                        PC.PlayerRespawnTransform.SetParent(null);
                        PC.dead = true;
                        
                        if (GameConstants.SelectedPlayerForGame.bodytype == "boy")
                        {
                            SoundManager.instance.ASPlayer.clip = SoundManager.instance.BoyDeathClip;
                            SoundManager.instance.ASPlayer.Play();
                        }
                        else
                        {
                            SoundManager.instance.ASPlayer.clip = SoundManager.instance.GirlDeathClip;
                            SoundManager.instance.ASPlayer.Play();
                        }
                        PC.MonsterAttackAnim();
                        //PC.CancelFunctionsInvoke();
                        PC.PlayerAnim.SetBool("Running", false);
                        PC.PlayerAnim.SetBool("Death", true);
                        PC.DisablePowerUps();
                        GameManager.instance.CurrentLives -= 1;
                        if (GameManager.instance.CurrentLives < 1)
                        {
                            if (GameConstants.GameType == "Tournament")
                            {
                                APIManager.instance.PostTournamentResultApi(GameConstants.JoinedTournamentId, (GameManager.instance.TotalDIstanceCovered / 1000).ToString(), GameManager.instance.TotalTimeSpend.ToString(), GameManager.instance.CollectedChillis.ToString());
                            }
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
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool && !PC.RespawnInvisibilityBool)
            {
                PC.MonsterMovement(2);
                if (PC.dead == false && PC.PortalUse == false)
                {
                    PC.PlayerRespawnTransform.SetParent(null);
                    PC.dead = true;
                    PC.DisablePowerUps();
                    if (GameConstants.SelectedPlayerForGame.bodytype == "boy")
                    {
                        SoundManager.instance.ASPlayer.clip = SoundManager.instance.BoyDeathClip;
                        SoundManager.instance.ASPlayer.Play();
                    }
                    else
                    {
                        SoundManager.instance.ASPlayer.clip = SoundManager.instance.GirlDeathClip;
                        SoundManager.instance.ASPlayer.Play();
                    }
                    PC.MonsterAttackAnim();
                    //PC.CancelFunctionsInvoke();
                    PC.PlayerAnim.SetBool("Running", false);
                    PC.PlayerAnim.SetBool("Death", true);
                    PC.DisablePowerUps();
                    GameManager.instance.CurrentLives -= 1;
                    if (GameManager.instance.CurrentLives < 1)
                    {
                        if (GameConstants.GameType == "Tournament")
                        {
                            APIManager.instance.PostTournamentResultApi(GameConstants.JoinedTournamentId, (GameManager.instance.TotalDIstanceCovered / 10000).ToString(), GameManager.instance.TotalTimeSpend.ToString(), GameManager.instance.CollectedChillis.ToString());
                        }
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
        PC.CancelFunctionsInvoke();
        
        UIManager.instance.GameOverPanelChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        UIManager.instance.GameOverPanelTimeHourText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Hours).ToString("00");
        UIManager.instance.GameOverPanelTimeMinuteText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Minutes).ToString("00");
        UIManager.instance.GameOverPanelTimeSecondsText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Seconds).ToString("00");
        UIManager.instance.GameOverPanelDistanceCoveredText.text = (GameManager.instance.TotalDIstanceCovered / 10000).ToString();
        UIManager.instance.GameOverPanel.SetActive(true);
        APIManager.instance.PostChilliesApi(GameManager.instance.CollectedChillis);
        if (GameConstants.GameType == "Tournament")
        {
            UIManager.instance.HomeButton.SetActive(false);
            UIManager.instance.RestartButton.SetActive(false);
            UIManager.instance.GameOverHomeButton.SetActive(true);
        }
        else
        {
            UIManager.instance.HomeButton.SetActive(true);
            UIManager.instance.RestartButton.SetActive(true);
            UIManager.instance.GameOverHomeButton.SetActive(false);
        }
        //SceneManager.LoadScene(0);
    }
}
