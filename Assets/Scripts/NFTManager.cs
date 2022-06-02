using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class NFTData
{
    public string ObjID;
    public string BoneName;
    public GameObject ObjRefrence;
}
public class NFTManager : MonoBehaviour
{
    public static NFTManager instance;
    public List<NFTData> NFTDataList;


    // Start is called before the first frame update
    void Start()
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

 
}
