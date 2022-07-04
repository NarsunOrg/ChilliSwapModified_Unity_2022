using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssetBundleManager : MonoBehaviour
{
    public static AssetBundleManager instance;
    public string[] url, sceneNames;
    static AssetBundle AssetBundleSceneOne;
    static AssetBundle AssetBundleSceneTwo;
    static AssetBundle AssetBundleSceneThree;
    static AssetBundle AssetBundleSceneFour;
    static AssetBundle AssetBundleSceneFive;
    WWW www;
    //// Start is called before the first frame update https://drive.google.com/uc?export=vi...
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
   
    public void playGamePressed(int i)
    {
        if(i == 1)
            StartCoroutine(ForSceneOne(i));
        if (i == 2)
            StartCoroutine(ForSceneTwo(i));
        if (i == 3)
            StartCoroutine(ForSceneThree(i));
        if (i == 4)
            StartCoroutine(ForSceneFour(i));
        if (i == 5)
            StartCoroutine(ForSceneFive(i));
    }

    IEnumerator ForSceneOne(int i)
    {
        if (!AssetBundleSceneOne)
        {
            Debug.Log("inside 11111");
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;
                }
                AssetBundleSceneOne = www.assetBundle;
            }
        }
        UISelectionManager.instance.SliderTime = 0.005f;
        string[] scenes = AssetBundleSceneOne.GetAllScenePaths();
        foreach (string s in scenes)
        {
            Debug.Log("inside 2222");
            print(Path.GetFileNameWithoutExtension(s));
            
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                Debug.Log("inside 333333");
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneTwo(int i)
    {
        if (!AssetBundleSceneTwo)
        {
            Debug.Log("inside 11111");
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;
                }
                AssetBundleSceneTwo = www.assetBundle;
            }
        }
        UISelectionManager.instance.SliderTime = 0.005f;
        string[] scenes = AssetBundleSceneTwo.GetAllScenePaths();
        foreach (string s in scenes)
        {
            Debug.Log("inside 2222");
            print(Path.GetFileNameWithoutExtension(s));
           
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                Debug.Log("inside 333333");
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneThree(int i)
    {
        if (!AssetBundleSceneThree)
        {
            Debug.Log("inside 11111");
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;
                }
                AssetBundleSceneThree = www.assetBundle;
            }
        }
        UISelectionManager.instance.SliderTime = 0.005f;
        string[] scenes = AssetBundleSceneThree.GetAllScenePaths();
        foreach (string s in scenes)
        {
            Debug.Log("inside 2222");
            print(Path.GetFileNameWithoutExtension(s));
          
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                Debug.Log("inside 333333");
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneFour(int i)
    {
        if (!AssetBundleSceneFour)
        {
            Debug.Log("inside 11111");
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;
                }
                AssetBundleSceneFour = www.assetBundle;
            }
        }
        UISelectionManager.instance.SliderTime = 0.005f;
        string[] scenes = AssetBundleSceneFour.GetAllScenePaths();
        foreach (string s in scenes)
        {
            Debug.Log("inside 2222");
            print(Path.GetFileNameWithoutExtension(s));
            
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                Debug.Log("inside 333333");
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneFive(int i)
    {
        if (!AssetBundleSceneFive)
        {
            Debug.Log("inside 11111");
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;
                }
                AssetBundleSceneFive = www.assetBundle;
            }
        }
        UISelectionManager.instance.SliderTime = 0.005f;
        string[] scenes = AssetBundleSceneFive.GetAllScenePaths();
        foreach (string s in scenes)
        {
            Debug.Log("inside 2222");
            print(Path.GetFileNameWithoutExtension(s));
            
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                Debug.Log("inside 333333");
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    public void loadScene(string name)
    {
       SceneManager.LoadScene(name);
    }
}
