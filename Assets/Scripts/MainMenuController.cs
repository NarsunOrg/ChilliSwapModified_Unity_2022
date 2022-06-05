using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    //public Text CharacterText;
    public GameObject cam;
    public List<GameObject> Maps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    //public void characterSetterForward()
    //{
    //    if (GameConstants.CharacterType == "Boy")
    //    {
    //        GameConstants.CharacterType = "Girl";
    //        //CharacterText.text = GameConstants.CharacterType;
    //        float angel = cam.transform.rotation.y + 180;
    //        cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
    //        Debug.Log(angel);
    //    }
    //    else if (GameConstants.CharacterType == "Girl")
    //    {
    //        GameConstants.CharacterType = "Boy";
    //        //CharacterText.text = GameConstants.CharacterType;
    //        float angel = cam.transform.rotation.y + 180;
    //        cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
    //        Debug.Log(angel);
    //    }
    //}
    //public void characterSetterBackward()
    //{
    //    if (GameConstants.CharacterType == "Boy")
    //    {
    //        GameConstants.CharacterType = "Girl";
    //        //CharacterText.text = GameConstants.CharacterType;
    //        float angel = cam.transform.rotation.y + 180;
    //        cam.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
    //        Debug.Log(angel);
    //    }
    //    else if (GameConstants.CharacterType == "Girl")
    //    {
    //        GameConstants.CharacterType = "Boy";
    //        //CharacterText.text = GameConstants.CharacterType;
    //        float angel = cam.transform.rotation.y + 180;
    //        cam.transform.DORotate(new Vector3(18, 180, 0), 0.5f);
    //        Debug.Log(angel);
    //    }
    //}

    public void ChangeMap(int a)
    {
        if (a > 0)
        {
            for (int i = 0; i < Maps.Count; i++)
            {
                if(Maps[i].activeSelf)
                {
                    Maps[i].SetActive(false);
                    Maps[(i + 1) % Maps.Count].SetActive(true);
                    break;
                }
            }
        }
        if (a < 0)
        {
            for (int i = 0; i < Maps.Count; i++)
            {
                if (Maps[i].activeSelf)
                {
                    Maps[i].SetActive(false);
                    if(i -1 < 0)
                    {
                        Maps[Maps.Count-1].SetActive(true);
                    }
                    else
                    {
                        Maps[i - 1].SetActive(true);
                    }
                    
                    break;
                }
            }
        }
    }
}
