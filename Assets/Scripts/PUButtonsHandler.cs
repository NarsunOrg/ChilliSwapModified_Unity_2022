using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUButtonsHandler : MonoBehaviour
{
    public PlayerController PURefrence;
    public List<Button> buttonsList;
    //Text currenttext = null;
    public bool powerupInUse;
    //float time;
    //int number;
    
    void Start()
    {
        //number = 0;
        powerupInUse = false;
        //time = 10;
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameConstants.SelectedPowerupNumber == 1 && UIManager.instance.PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable == true)
        {
            setPowerUpSuperJump(1);
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameConstants.SelectedPowerupNumber == 2 && UIManager.instance.PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable == true)
        {
            setPowerUpSuperSpeed(2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameConstants.SelectedPowerupNumber == 3 && UIManager.instance.PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable == true)
        {
            setPowerUpTeleportation(3);
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameConstants.SelectedPowerupNumber == 4 && UIManager.instance.PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable == true)
        {
            setPowerUpSlowingDown(4);
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameConstants.SelectedPowerupNumber == 5 && UIManager.instance.PowerupsButtons[GameConstants.SelectedPowerupNumber].GetComponent<Button>().interactable == true)
        {
            setPowerUpLaserGoggles(5);
        }


        //if (PURefrence.dead)
        //{
        //    for (int i = 0; i < buttonsList.Count; i++)
        //    {
        //        buttonsList[i].interactable = true;
        //    }
        //    //if (currenttext.text != null)
        //    //{
        //    //    currenttext.text = (number + 1).ToString();

        //    //}
        //    time = 10;
        //    powerupInUse = false;
        //}

        //if(powerupInUse)
        //{
        //    time = time - Time.deltaTime;
        //    currenttext.text = ((int)time).ToString();
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad1) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpInvisibility(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad2) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpSuperJump(1);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad3) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpSuperSpeed(2);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad4) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpSlowingDown(3);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad5) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpTeleportation(4);
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad6) && !powerupInUse && !PURefrence.dead)
        //{
        //    setPowerUpLaserGoggles(5);
        //}
    }
    public void setPowerUpInvisibility(int btnNmbr)
    {
        //powerupInUse = true;
        if(UIManager.instance.pc.isonturn==false)
        PURefrence.Invisibility();
        //StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSuperJump(int btnNmbr)
    {
        //powerupInUse = true;
        if (UIManager.instance.pc.isonturn == false)

            PURefrence.SuperJump();
        //StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSuperSpeed(int btnNmbr)
    {
        //powerupInUse = true;
        if(UIManager.instance.pc.isonturn==false)
         PURefrence.SuperSpeed();
        //StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSlowingDown(int btnNmbr)
    {
        //powerupInUse = true;
        if (UIManager.instance.pc.isonturn == false)

            PURefrence.SlowingDown();

        //StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpTeleportation(int btnNmbr)
    {
        //powerupInUse = true;
        if (UIManager.instance.pc.isonturn == false)

            PURefrence.Teleportation();
        //StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpLaserGoggles(int btnNmbr)
    {
        //powerupInUse = true;
        if (UIManager.instance.pc.isonturn == false)

            PURefrence.LaserGoggles();
        //StartCoroutine(EndPowerup(btnNmbr));
    }
    //IEnumerator EndPowerup(int nmbr)
    //{
    //    number = nmbr;
    //    yield return new WaitForSeconds(10);
    //    for (int i = 0; i < buttonsList.Count; i++)
    //    {
    //        buttonsList[i].interactable = true;
    //    }
    //    currenttext.text = (nmbr + 1).ToString();
    //    time = 10;
    //    powerupInUse = false;
    //}
}
