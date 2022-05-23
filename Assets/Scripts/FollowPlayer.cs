using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject PositionToSet;
    public Vector3 offset;
    public static float lookatspeed=1f;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        gameObject.transform.DOLocalMove(PositionToSet.transform.position, 0.2f);
        gameObject.transform.DOLookAt(Player.transform.position, lookatspeed);
    }
}
