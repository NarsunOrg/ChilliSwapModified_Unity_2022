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
        if (GameConstants.CharacterType == "Boy")
        {
            gameObject.transform.DOMove((BoyHead.transform.position), 0.01f);
        }
        if (GameConstants.CharacterType == "Girl")
        {
            gameObject.transform.DOMove((BoyHead.transform.position), 0.01f);
        }
    }
}
