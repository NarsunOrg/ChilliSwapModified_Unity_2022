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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        
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
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield break;
                }
                AssetBundleSceneOne = www.assetBundle;
            }
        }
        //UISelectionManager.instance.SliderTime = 1f;
        UISelectionManager.instance.LoadingPanel.SetActive(false);
        string[] scenes = AssetBundleSceneOne.GetAllScenePaths();
        foreach (string s in scenes)
        {
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneTwo(int i)
    {
        if (!AssetBundleSceneTwo)
        {
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield break;
                }
                AssetBundleSceneTwo = www.assetBundle;
            }
        }
        //UISelectionManager.instance.SliderTime = 1f;
        UISelectionManager.instance.LoadingPanel.SetActive(false);
        string[] scenes = AssetBundleSceneTwo.GetAllScenePaths();
        foreach (string s in scenes)
        {
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneThree(int i)
    {
        if (!AssetBundleSceneThree)
        {
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield break;
                }
                AssetBundleSceneThree = www.assetBundle;
            }
        }
        //UISelectionManager.instance.SliderTime = 1f;
        UISelectionManager.instance.LoadingPanel.SetActive(false);
        string[] scenes = AssetBundleSceneThree.GetAllScenePaths();
        foreach (string s in scenes)
        {
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneFour(int i)
    {
        if (!AssetBundleSceneFour)
        {
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield break;
                }
                AssetBundleSceneFour = www.assetBundle;
            }
        }
        //UISelectionManager.instance.SliderTime = 1f;
        UISelectionManager.instance.LoadingPanel.SetActive(false);
        string[] scenes = AssetBundleSceneFour.GetAllScenePaths();
        foreach (string s in scenes)
        {
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    IEnumerator ForSceneFive(int i)
    {
        if (!AssetBundleSceneFive)
        {
            using (www = new WWW(url[i]))
            {
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    yield break;
                }
                AssetBundleSceneFive = www.assetBundle;
            }
        }
        //UISelectionManager.instance.SliderTime = 1f;
        UISelectionManager.instance.LoadingPanel.SetActive(false);
        string[] scenes = AssetBundleSceneFive.GetAllScenePaths();
        foreach (string s in scenes)
        {
            if (Path.GetFileNameWithoutExtension(s) == sceneNames[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }

    public void loadScene(string name)
    {
       SceneManager.LoadScene(name);
    }
}
