using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUButtonsHandler : MonoBehaviour
{
    public PlayerController PURefrence;
    public List<Button> buttonsList;
    Text currenttext;
    bool powerupInUse;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        powerupInUse = false;
        time = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(powerupInUse)
        {
            time = time - Time.deltaTime;
            currenttext.text = ((int)time).ToString();
        }
    }
    public void setPowerUpInvisibility(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.Invisibility();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSuperJump(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.SuperJump();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSuperSpeed(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.SuperSpeed();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpSlowingDown(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.SlowingDown();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpTeleportation(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.Teleportation();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    public void setPowerUpLaserGoggles(int btnNmbr)
    {
        powerupInUse = true;
        currenttext = buttonsList[btnNmbr].transform.GetChild(0).GetComponent<Text>();
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = false;
        }
        PURefrence.LaserGoggles();
        StartCoroutine(EndPowerup(btnNmbr));
    }
    IEnumerator EndPowerup(int nmbr)
    {
        yield return new WaitForSeconds(10);
        for (int i = 0; i < buttonsList.Count; i++)
        {
            buttonsList[i].interactable = true;
        }
        currenttext.text = (nmbr + 1).ToString();
        time = 10;
        powerupInUse = false;
    }
}
