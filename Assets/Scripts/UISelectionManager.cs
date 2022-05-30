using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISelectionManager : MonoBehaviour
{
    public static UISelectionManager instance;
    public GameObject EnvironmentsSelectionPanel;
    public GameObject LeaderboardPanel;
    public string TournamentId;
    public Text UserTokens;
    public Text UserTotalCollectedChillies;
    public Text EnvNameText;
    public int EnvNumber = 1;
    public GameObject[] EnvImagesList;
    public GameObject cam;


    void Start()
    {
        instance = this;
        if (GameConstants.GameType == "Single")
        {
            LeaderboardPanel.SetActive(false);
        }
        else
        {
            LeaderboardPanel.SetActive(true);
        }
    }

    public void OnSingleButton()
    {
        GameConstants.GameType = "Single";
    }

    public void OnTournamentButton()
    {
        GameConstants.GameType = "Tournament";
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
        if (GameConstants.CharacterType == "Boy")
        {
            GameConstants.CharacterType = "Girl";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
            Debug.Log(angel);
        }
    }

    public void OnCharacterSelectionBackwardButton()
    {
        if (GameConstants.CharacterType == "Boy")
        {
            GameConstants.CharacterType = "Girl";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
            Debug.Log(angel);
        }
    }

    public void OnPowerUpSelectButton()
    {
        SceneManager.LoadScene(EnvNumber);
    }
}
