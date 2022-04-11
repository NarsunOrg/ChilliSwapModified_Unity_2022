using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject PositionToSet;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = new Vector3(Player.transform.position.x + offset.x, Player.transform.position.y + offset.y, Player.transform.position.z + offset.z);
        gameObject.transform.DOLocalMove(PositionToSet.transform.position, 1);
        //gameObject.transform.DOLocalRotate(new Vector3(PositionToSet.transform.rotation.x + 25, PositionToSet.transform.rotation.y, PositionToSet.transform.rotation.z), 1);
        transform.LookAt(Player.transform);
    }
}
