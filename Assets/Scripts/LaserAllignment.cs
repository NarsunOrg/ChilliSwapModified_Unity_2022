using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserAllignment : MonoBehaviour
{
    public GameObject BoyHead, GirlHead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameConstants.SelectedPlayerForGame.bodytype == "boy")
        {
            gameObject.transform.DOMove((BoyHead.transform.position), 0.01f);
        }
        if (GameConstants.SelectedPlayerForGame.bodytype == "girl")
        {
            gameObject.transform.DOMove((BoyHead.transform.position), 0.01f);
        }
    }
}
