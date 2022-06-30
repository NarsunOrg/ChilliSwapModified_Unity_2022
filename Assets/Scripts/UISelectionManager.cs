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
    public Text StoreCharacterNameText;
    public Text OutfitPanelCharacterNameText;
    public Text SkinsPanelCharacterNameText;
    public Transform TournamentScrollContent;
    public GameObject[] PowerupsSelectButtons;
    public int Sound = 1;
    public int Music = 1;
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    public float SliderTime = 1f;
    public bool[] IsPowerUpSelected;
    public GameObject EnvForwardBUtton;
    public GameObject EnvBackwardBUtton;

    private void Awake()
    {
        instance = this;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }
    void Start()
    {
        //instance = this;
        GameConstants.CharacterType = "boy";
        StartCoroutine(LoadingBar());
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
            if (EnvNumber == 5)
            {
                EnvForwardBUtton.SetActive(false);
                EnvBackwardBUtton.SetActive(true);
            }
            if (EnvNumber == 2)
            {
                EnvBackwardBUtton.SetActive(true);
            }
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
            if (EnvNumber == 1)
            {
                EnvForwardBUtton.SetActive(true);
                EnvBackwardBUtton.SetActive(false);
            }
            if (EnvNumber == 4)
            {
                EnvForwardBUtton.SetActive(true);
            }
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
           // Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "girl")
        {
            GameConstants.CharacterType = "boy";
            CharacterNameText.text = "MIKE";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
           // Debug.Log(angel);
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
           // Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "girl")
        {
            GameConstants.CharacterType = "boy";
            CharacterNameText.text = "MIKE";
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
         //   Debug.Log(angel);
        }
    }

    public void OnPowerUpSelectButton()
    {
        //SceneManager.LoadScene(EnvNumber);
        AssetBundleManager.instance.playGamePressed(EnvNumber);
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
        //PowerupsSelectButtons[powerupindex].GetComponent<Button>().interactable = false;
        if (IsPowerUpSelected[powerupindex] == true)
        {
            PowerupsSelectButtons[powerupindex].GetComponentInChildren<Text>().text = "SELECT";
            GameConstants.SelectedPowerupNumber = 0;
            IsPowerUpSelected[powerupindex] = false;
        }
        else
        {
            PowerupsSelectButtons[powerupindex].GetComponentInChildren<Text>().text = "SELECTED";
            GameConstants.SelectedPowerupNumber = powerupindex;
            for (int i = 1; i < IsPowerUpSelected.Length; i++)
            {
                IsPowerUpSelected[i] = false;
            }
            IsPowerUpSelected[powerupindex] = true;
        }
    }

    public void OnSoundButtonOn()
    {
        SoundManager.instance.AS.enabled = false;
        SoundManager.instance.ASChillies.enabled = false;
        SoundManager.instance.ASPlayer.enabled = false;
        Sound = 0;
    }

    public void OnSoundButtonOff()
    {
        SoundManager.instance.AS.enabled = true;
        SoundManager.instance.ASChillies.enabled = true;
        SoundManager.instance.ASPlayer.enabled = true;
        Sound = 1;
    }

    public void OnMusicButtonOn()
    {
        SoundManager.instance.ASBg.enabled = false;
        Music = 0;
    }

    public void OnMusicButtonOff()
    {
        SoundManager.instance.ASBg.enabled = true;
        Music = 1;
    }

    IEnumerator LoadingBar()
    {
        yield return new WaitForSeconds(SliderTime);
        if (LoadingSlider.value < 0.9)
        {
            LoadingSlider.value += 0.01f;
            StartCoroutine(LoadingBar());
        }
        else
        {
            LoadingPanel.SetActive(false);
        }
    }

    public void StoreBackButton()
    {
        SlotManager.instance.BoyskintoneScreen.SetActive(false);
        SlotManager.instance.BoyhairstyleScreen.SetActive(false);
        SlotManager.instance.BoyeyecolorScreen.SetActive(false);
        SlotManager.instance.BoyclothesScreen.SetActive(false);
        SlotManager.instance.BoyheadwearScreen.SetActive(false);
        SlotManager.instance.BoygogglesScreen.SetActive(false);
        SlotManager.instance.BoyheadphonesScreen.SetActive(false);
        SlotManager.instance.BoyshoesScreen.SetActive(false);
        SlotManager.instance.BoybackpackScreen.SetActive(false);
        SlotManager.instance.BoywatchScreen.SetActive(false);

        SlotManager.instance.GirlskintoneScreen.SetActive(false);
        SlotManager.instance.GirlhairstyleScreen.SetActive(false);
        SlotManager.instance.GirleyecolorScreen.SetActive(false);
        SlotManager.instance.GirlclothesScreen.SetActive(false);
        SlotManager.instance.GirlheadwearScreen.SetActive(false);
        SlotManager.instance.GirlgogglesScreen.SetActive(false);
        SlotManager.instance.GirlheadphonesScreen.SetActive(false);
        SlotManager.instance.GirlshoesScreen.SetActive(false);
        SlotManager.instance.GirlbackpackScreen.SetActive(false);
        SlotManager.instance.GirlwatchScreen.SetActive(false);

        SlotManager.instance.PlayerRotatingSlider.value = 0;
        foreach (GameObject g in SlotManager.instance.CharacterList)
        {
            g.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
