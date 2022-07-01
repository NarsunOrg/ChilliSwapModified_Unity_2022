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
    static AssetBundle assetBundle;
    WWW www;
    //// Start is called before the first frame update https://drive.google.com/uc?export=vi...
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        //playGamePressed(0);
    }

    public void playGamePressed(int i)
    {

        StartCoroutine(s(i));
    }

    IEnumerator s(int i)
    {
        if (!assetBundle)
        {
            using (www = new WWW(url[i]))
            {
                //loadingStart = true;
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;

                }
                assetBundle = www.assetBundle;

            }
        }
        //loadingStart = false;
        string[] scenes = assetBundle.GetAllScenePaths();

        foreach (string s in scenes)
        {
            //print(Path.GetFileNameWithoutExtension(s));
            //print(Path.GetFileNameWithoutExtension(s));
            //loadScene(Path.GetFileNameWithoutExtension(s));
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
