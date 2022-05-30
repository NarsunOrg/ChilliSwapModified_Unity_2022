using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OffsetFollower : MonoBehaviour
{
    public GameObject dummy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.DOLocalMoveX(dummy.transform.localPosition.x, 0.1f);
        gameObject.transform.DOLocalMoveY(dummy.transform.localPosition.y + 3.5f, 0.5f);
    }
}
