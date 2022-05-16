using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //public Text CharacterText;
    public GameObject boy, girl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void characterSetter()
    {
        if (GameConstants.CharacterType == "Boy")
        {
            GameConstants.CharacterType = "Girl";
            //CharacterText.text = GameConstants.CharacterType;
            girl.SetActive(true);
            boy.SetActive(false);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            boy.SetActive(true);
            girl.SetActive(false);
        }
    }
}
