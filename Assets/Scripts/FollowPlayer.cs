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
    float sSpeed = 1f;
    
    //void Update()
    //{
    //    this.transform.position = PositionToSet.transform.position;
    //   // transform.LookAt(Player.transform);
    //    //this.transform.rotation = PositionToSet.transform.rotation;
    //    // gameObject.transform.DOLocalMove(PositionToSet.transform.position, 0.2f);
    //    //gameObject.transform.DOLookAt(Player.transform.position, lookatspeed);
    //}
    // camera will follow this object
    public Transform Target;
    //camera transform
   // public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.01f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
      //  Offset = this.transform.position - Target.position;
    }

    private void LateUpdate()
    {
        // update position
        //Vector3 targetPosition = Target.position + Offset;
        this.transform.position = Vector3.SmoothDamp(transform.position, PositionToSet.transform.position, ref velocity, SmoothTime);

        // update rotation
        transform.LookAt(Target);
    }

}
