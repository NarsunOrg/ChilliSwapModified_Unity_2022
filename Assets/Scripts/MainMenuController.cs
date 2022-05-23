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
    public void characterSetterForward()
    {
        if (GameConstants.CharacterType == "Boy")
        {
            GameConstants.CharacterType = "Girl";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, angel, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, angel, 0), 0.5f);
            Debug.Log(angel);
        }
    }
    public void characterSetterBackward()
    {
        if (GameConstants.CharacterType == "Boy")
        {
            GameConstants.CharacterType = "Girl";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, angel, 0), 0.5f);
            Debug.Log(angel);
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            GameConstants.CharacterType = "Boy";
            //CharacterText.text = GameConstants.CharacterType;
            float angel = cam.transform.rotation.y + 180;
            cam.transform.DORotate(new Vector3(18, angel, 0), 0.5f);
            Debug.Log(angel);
        }
    }
}
