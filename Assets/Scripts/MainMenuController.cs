using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    //public Text CharacterText;
    public GameObject cam;
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
            cam.transform.DOLocalRotate(new Vector3(18, 0, 0), 0.5f);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            cam.transform.DOLocalRotate(new Vector3(18, 180, 0), 0.5f);
        }
    }
}
