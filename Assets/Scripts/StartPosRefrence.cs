using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartPosRefrence : MonoBehaviour
{
    public GameObject NextPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if(other.transform.gameObject.GetComponent<PlayerController>().SuperSpeedBool)
            {
                other.transform.gameObject.GetComponent<PlayerController>().State = NextPosition.transform.tag;
                other.transform.parent.DOLocalMove(NextPosition.transform.position, 1);
            }
            else
            {
                other.transform.gameObject.GetComponent<PlayerController>().EnteredPlatformTrigger(NextPosition);
            }
        }
    }
}
