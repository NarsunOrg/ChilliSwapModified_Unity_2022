using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISelectionManager : MonoBehaviour
{
    public static UISelectionManager instance;
    public GameObject ProfileInnerPanel;
    public GameObject SwapChilliMessagePanel;
    public GameObject EnvironmentsSelectionPanel;
    public Text UserTokens;
    public Text UserTotalCollectedChillies;
    public Text EnvNameText;
    public int EnvNumber = 1;
    public GameObject[] EnvImagesList;
    public GameObject cam;
    public Text CharacterNameText;
    public Transform TournamentScrollContent;
    public GameObject[] PowerupsSelectButtons;


    void Start()
    {
        instance = this;
        GameConstants.CharacterType = "boy";
    }

    public void OnSingleButton()
    {
        GameConstants.GameType = "Single";
    }

    public void OnTournamentButton()
    {
        GameConstants.GameType = "Tournament";
        APIManager.instance.GetAllTournamentsAPI();
    }

    public void OnProfileSwapButton()
    {
        APIManager.instance.GetSwapChilliesApi();
    }

    public void OnEnvSelectionForwardButton()
    {
        if (EnvNumber < 5)
        {
            EnvNumber++;
            foreach (GameObject g in EnvImagesList)
            {
                g.SetActive(false);
            }
            EnvImagesList[EnvNumber].SetActive(true);
            EnvNameText.text = EnvImagesList[EnvNumber].name;
        }
    }

    public void OnEnvSelectionBackwardButton()
    {
        if (EnvNumber > 1)
        {
            EnvNumber--;
            foreach (GameObject g in EnvImagesList)
            {
                g.SetActive(false);
            }
            EnvImagesList[EnvNumber].SetActive(true);
            EnvNameText.text = EnvImagesList[EnvNumber].name;
        }
    }

    public void OnCharacterSelectionForwardButton()
    {
        if (GameConstants.CharacterType == "boy")
        {
            GameConstants.CharacterType = "girl";
            CharacterNameText.text = "SARAH";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "girl")
        {
            GameConstants.CharacterType = "boy";
            CharacterNameText.text = "MIKE";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
            Debug.Log(angel);
        }
    }

    public void OnCharacterSelectionBackwardButton()
    {
        if (GameConstants.CharacterType == "boy")
        {
            GameConstants.CharacterType = "girl";
            CharacterNameText.text = "SARAH";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "girl")
        {
            GameConstants.CharacterType = "boy";
            CharacterNameText.text = "MIKE";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
            Debug.Log(angel);
        }
    }

    public void OnPowerUpSelectButton()
    {
        SceneManager.LoadScene(EnvNumber);
        GameConstants.SceneLoaded = EnvNumber;
    }

    public void OnCloseStore()
    {
        APIManager.instance.SetCharacterApi();
    }

    public void OnSelectPowerup(int powerupindex)
    {
        foreach (GameObject g in PowerupsSelectButtons)
        {
            g.GetComponent<Button>().interactable = true;
            g.GetComponentInChildren<Text>().text = "SELECT";
        }
        PowerupsSelectButtons[powerupindex].GetComponent<Button>().interactable = false;
        PowerupsSelectButtons[powerupindex].GetComponentInChildren<Text>().text = "SELECTED";
        GameConstants.SelectedPowerupNumber = powerupindex;
       
    }
}
