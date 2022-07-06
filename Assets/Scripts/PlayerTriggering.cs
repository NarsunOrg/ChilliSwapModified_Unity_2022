using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggering : MonoBehaviour
{
    public Animation anim; 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            anim.Play();
        }
    }
}
