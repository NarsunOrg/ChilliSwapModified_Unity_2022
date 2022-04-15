using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagmentScript : MonoBehaviour
{
    public void StartScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
