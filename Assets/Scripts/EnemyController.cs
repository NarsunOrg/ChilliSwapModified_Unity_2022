using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(0, 0, -3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StumbleFollow()
    {
        transform.DOLocalMoveZ(-3, 1);
    }
    public void NormalFollow()
    {
        transform.DOLocalMoveZ(-5, 1);
    }
    public void PlayerDeadStand()
    {
        transform.DOLocalMoveZ(-1, 1);
    }
}
